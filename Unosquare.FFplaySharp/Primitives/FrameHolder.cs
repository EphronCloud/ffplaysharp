﻿namespace Unosquare.FFplaySharp.Primitives
{
    using FFmpeg.AutoGen;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public unsafe sealed class FrameHolder : IDisposable
    {
        public FrameHolder()
        {
            FramePtr = ffmpeg.av_frame_alloc();
        }

        public AVFrame* FramePtr { get; private set; }

        public AVSubtitle* SubtitlePtr { get; set; }

        public int Serial;
        
        /// <summary>
        /// Gets or sets the Presentation time in seconds.
        /// This is NOT a timestamp in stream units.
        /// </summary>
        public double Time { get; set; }

        public double Duration;      /* estimated duration of the frame */
        public long Position;          /* byte position of the frame in the input file */
        public int Width;
        public int Height;
        public int Format;
        public AVRational Sar;
        public bool uploaded;
        public bool FlipVertical;

        public AVSampleFormat SampleFormat => (AVSampleFormat)FramePtr->format;

        public string SampleFormatName => AudioParams.GetSampleFormatName(SampleFormat);

        public AVPixelFormat PixelFormat => (AVPixelFormat)FramePtr->format;

        public int PixelWidth => FramePtr->width;

        public int PixelHeight => FramePtr->height;

        public byte_ptrArray8 PixelData => FramePtr->data;

        public int_array8 PixelStride => FramePtr->linesize;

        public int Channels => FramePtr->channels;

        public int Frequency => FramePtr->sample_rate;

        public int SampleCount => FramePtr->nb_samples;

        public bool HasValidTime => !Time.IsNaN();

        public void Unreference()
        {
            if (FramePtr != null)
                ffmpeg.av_frame_unref(FramePtr);

            if (SubtitlePtr != null)
            {
                ffmpeg.avsubtitle_free(SubtitlePtr);
                SubtitlePtr = null;
            }
        }

        public void Dispose()
        {
            var framePtr = FramePtr;
            if (framePtr != null)
            {
                ffmpeg.av_frame_unref(FramePtr);
                ffmpeg.av_frame_free(&framePtr);
                FramePtr = null;
            }

            if (SubtitlePtr != null)
            {
                ffmpeg.avsubtitle_free(SubtitlePtr);
                SubtitlePtr = null;
            }

            SubtitlePtr = null;
        }
    }
}
