﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Audio;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Framework.Tests.Visual
{
    internal class TestCaseWaveform : FrameworkTestCase
    {
        private readonly List<WaveformGraph> waveforms = new List<WaveformGraph>();

        public TestCaseWaveform()
        {
            FillFlowContainer flow;
            Add(flow = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Both,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10)
            });

            for (int i = 1; i <= 16; i *= 2)
            {
                var newDisplay = new WaveformGraph
                {
                    RelativeSizeAxes = Axes.Both,
                    Resolution = 1f / i
                };

                waveforms.Add(newDisplay);

                flow.Add(new Container
                {
                    RelativeSizeAxes = Axes.X,
                    Height = 100,
                    Children = new Drawable[]
                    {
                        newDisplay,
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black,
                                    Alpha = 0.75f
                                },
                                new SpriteText
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Padding = new MarginPadding(4),
                                    Text = $"Resolution: {1f / i:0.00}"
                                }
                            }
                        }
                    }
                });
            }
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio)
        {
            var track = audio.GetTrackManager().Get("sample-track");
            waveforms.ForEach(w => w.Track = track);
        }
    }
}
