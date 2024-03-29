﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MLib.Multimedia.VLC
{
    [ComVisible(true)]
    [Guid("B5A4F6A1-B8C8-4f4f-B9CD-396DCDCAC4C3")]
    public struct TrackPosition : IComparable<TrackPosition>
    {
        public int time;
        public double position;

        public TrackPosition(int time, double position)
        {
            this.time = time;
            this.position = position;
        }

        public TrackPosition(TrackPosition other)
        {
            this.time = other.time;
            this.position = other.position;
        }

        #region IComparable<TrackPosition> Members

        public int CompareTo(TrackPosition other)
        {
            return this.time.CompareTo(other.time);
        }

        #endregion
    }

    [ComVisible(true)]
    public enum PlayerState
    {
        None = 0,
        Playing = 1,
        Paused = 2,
    }

    [ComVisible(true)]
    public enum MetaData
    {
        NowPlaying = 0,
    }

    public class MetaDataUpdateEventArgs : EventArgs
    {
        MetaData data;
        String text;

        public MetaDataUpdateEventArgs(MetaData data, String text)
        {
            this.data = data;
            this.text = text;
        }

        public MetaData Data { get { return this.data; } }
        public String Text { get { return this.text; } }
    }

    public delegate void MetaDataEventHandler(object sender, MetaDataUpdateEventArgs args);

    [ComVisible(true)]
    [Guid("2EA6BBFA-8BDE-4023-AEE8-916694614B25")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IPlayer : IDisposable
    {
        bool Visible { get; set; }
        Control Parent { get; set; }
        Rectangle Bounds { get; set; }
        Point Location { get; set; }
        Size Size { get; set; }
        Size VideoSize { get; }
        int Time { get; }
        double Position { get; }
        void MoveToPosition(TrackPosition newTrackPosition);
        int Volume { get; set; }
        int Rate { get; set; }
        void GetRates(out int minRate, out int maxRate, out int normalRate);
        bool IsPlaying { get; }
        bool IsPaused { get; }
        bool IsMute { get; }
        int Length { get; set; }
        PlayerState State { get; }
        double TimeScaling { get; set; }

        void Play();
        void ToggleMute();
        void TogglePause();
        void RotateSubtitles();
        void RotateAudioTrack();
        void RotateDeinterlaceMode();
        void RotateAspectRatio();
        void RotateCropModes();
        bool UseMpegVbrOffset { get; set; }
        void CropTop();
        void UnCropTop();
        void CropBottom();
        void UnCropBottom();
        void CropLeft();
        void UnCropLeft();
        void CropRight();
        void UnCropRight();
        void NextDvdTrack();
        void PreviousDvdTrack();
        void NextDvdChapter();
        void PreviousDvdChapter();
        TrackPosition Shuttle(int offsetSeconds);
        void ClearPlayList();
        int AddToPlayList(String fileName, String title, String[] options);
        void PlayItem(int index);
        void Stop();
    }

    [ComVisible(true)]
    [Guid("5CCB86E4-FFD0-49a0-8805-EE16A79CED50")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IPlayer2 : IPlayer
    {
        String DeinterlaceMode { get; set; }
        String AspectRatio { get; set; }
        String CropMode { get; set; }
        int CroppingLeft { get; set; }
        int CroppingRight { get; set; }
        int CroppingTop { get; set; }
        int CroppingBottom { get; set; }
        int AudioTrack { get; set; }
        int SubTitleTrack { get; set; }
        // in ms
        int AudioDelay { get; set; }
        int SubTitleDelay { get; set; }
        int ChapterCount { get; }
        int Chapter { get; set; }
        int Program { get; set; }

        bool AllowVideoAdjustments { get; set; }	// needs to be on to allow Contrast, Brightness, etc. to be adjusted
        float Contrast { get; set; }	// range from 0 to 2, default 1
        float Brightness { get; set; }	// range from 0 to 2, default 1
        int Hue { get; set; }			// range from 0 to 360, default 0
        float Saturation { get; set; }	// range from 0 to 3, default 1
        float Gamma { get; set; }		// range from .01 to 10, default 1

        void AddAndPlay(String fileName, String options);

        String DeinterlaceModesAsString();
        void DeinterlaceModes(out String[] choices, out String[] choiceText);
        String AspectRatiosAsString();
        void AspectRatios(out String[] choices, out String[] choiceText);
        String CropModesAsString();
        void CropModes(out String[] choices, out String[] choiceText);
        String AudioTracksAsString();
        void AudioTracks(out int[] trackIds, out String[] trackNames);
        String SubTitleTracksAsString();
        void SubTitleTracks(out int[] trackIds, out String[] trackNames);
        String ProgramsAsString();
        void Programs(out int[] trackIds, out String[] trackNames);
        void DisplayMessage(String message);
        String GetConfigVariable(String name, String returnOnError);
        bool SetConfigVariable(String name, String value);
        void PrecomputeCrop(Size videoSize, int cropLeft, int cropRight, int cropTop, int cropBottom);
        bool ComputeCrop();

        event MetaDataEventHandler NowPlaying;
    }
}
