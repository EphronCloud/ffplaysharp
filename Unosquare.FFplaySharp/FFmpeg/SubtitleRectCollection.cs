﻿namespace FFmpeg;

public unsafe sealed class SubtitleRectCollection : ChildCollection<FFSubtitle, FFSubtitleRect>
{
    public SubtitleRectCollection(FFSubtitle parent)
        : base(parent)
    {
        // placeholder
    }

    public override FFSubtitleRect this[int index]
    {
        get => new(Parent.Target->rects[index]);
        set => Parent.Target->rects[index] = value.IsNotNull() ? value.Target : default;
    }

    public override int Count =>
        Convert.ToInt32(Parent.Target->num_rects);
}
