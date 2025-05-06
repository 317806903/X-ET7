using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEditor;
using UnityEngine.Bindings;

namespace TATools.AudioFormatSetting
{
    public class AudioImportSettingTemplate
    {
        public AudioImporter audioImporter { get; set; }

        public AudioImportSettingTemplate(AudioImporter audioImporter)
        {
            this.audioImporter = audioImporter;
        }
    }

    public class AudioImportSettingTemplateHelper
    {
        public AudioImportSettingTemplate template;

        public AudioImportSettingTemplateHelper(AudioCatalog audioCatalog, AudioImporter audioImporter)
        {
            this.template = GetImportSettingTemplate(audioCatalog, audioImporter);
        }

        AudioImportSettingTemplate GetImportSettingTemplate(AudioCatalog audioCatalog, AudioImporter audioImporter)
        {
            switch (audioCatalog)
            {
                case AudioCatalog.NotMatch:
                    return null;
                case AudioCatalog.NotDeal:
                    return null;
            }

            string path = audioImporter.assetPath;
            AudioImportSettingTemplate temp = new AudioImportSettingTemplate(audioImporter);

            switch (audioCatalog)
            {
                case AudioCatalog.AduioBattle:
                {

                    AudioImporterSampleSettings audioSetting = new ();
                    //加载方式选择
                    audioSetting.loadType = AudioClipLoadType.DecompressOnLoad;
                    //压缩方式选择
                    audioSetting.compressionFormat = AudioCompressionFormat.ADPCM;
                    //设置播放质量
                    audioSetting.quality = 1f;
                    //优化采样率
                    audioSetting.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;
                    audioSetting.preloadAudioData = false;

                    //开启单声道
                    audioImporter.forceToMono = false;
                    audioImporter.defaultSampleSettings = audioSetting;

                }
                    break;
                case AudioCatalog.AduioUI:
                {

                    AudioImporterSampleSettings audioSetting = new ();
                    //加载方式选择
                    audioSetting.loadType = AudioClipLoadType.DecompressOnLoad;
                    //压缩方式选择
                    audioSetting.compressionFormat = AudioCompressionFormat.Vorbis;
                    //设置播放质量
                    audioSetting.quality = 1f;
                    //优化采样率
                    audioSetting.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;
                    audioSetting.preloadAudioData = false;

                    //开启单声道
                    audioImporter.forceToMono = true;
                    audioImporter.defaultSampleSettings = audioSetting;

                }
                    break;
                case AudioCatalog.Music:
                {

                    AudioImporterSampleSettings audioSetting = new ();
                    //加载方式选择
                    audioSetting.loadType = AudioClipLoadType.Streaming;
                    //压缩方式选择
                    audioSetting.compressionFormat = AudioCompressionFormat.Vorbis;
                    //设置播放质量
                    audioSetting.quality = 1f;
                    //优化采样率
                    audioSetting.sampleRateSetting = AudioSampleRateSetting.OptimizeSampleRate;
                    audioSetting.preloadAudioData = false;

                    //开启单声道
                    audioImporter.forceToMono = true;
                    audioImporter.defaultSampleSettings = audioSetting;

                }
                    break;
            }

            return temp;
        }

    }
}