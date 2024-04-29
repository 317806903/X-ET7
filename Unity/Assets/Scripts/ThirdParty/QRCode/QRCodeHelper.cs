using System;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.Common;

namespace ET
{
    public static class QRCodeHelper
    {
        #region 生成二维码

        /// <summary>
        /// 生成二维码
        /// 正方形
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="length">宽度</param>
        /// <returns></returns>
        public static Texture2D CreateQRCode(string content, int length)
        {
            Color32[] colors = GenerateQRCode(content, length);
            Texture2D texture = new Texture2D(length, length);
            texture.SetPixels32(colors);
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// 生成二维码
        /// 正方形
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="length">宽度</param>
        /// <returns></returns>
        public static Color32[] GenerateQRCode(string content, int length)
        {
            // 编码成color32
            EncodingOptions options = null;
            BarcodeWriter writer = new BarcodeWriter();
            options = new EncodingOptions { Width = length, Height = length, Margin = 1, };
            options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            Color32[] colors = writer.Write(content);
            return colors;
        }

        #endregion

        #region 图片识别

        /// <summary>
        /// 识别二维码
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Result ScanQRCode(Texture2D texture)
        {
            return ScanQRCode(texture.GetPixels32(), texture.width, texture.height);
        }

        /// <summary>
        /// 识别二维码
        /// </summary>
        /// <param name="textureData"></param>
        /// <param name="textureDataWidth"></param>
        /// <param name="textureDataHeight"></param>
        /// <returns></returns>
        public static Result ScanQRCode(Color32[] textureData, int textureDataWidth, int textureDataHeight)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(textureData, textureDataWidth, textureDataHeight);

            return result;
        }

        #endregion
    }
}