using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Dev.A4.Enums;

namespace Dev.A4.General
{
    public static class cUtility
    {
        public const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        public const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string NUMBER = "0123456789";

        public static string CorrectPath(string i_sPath)
        {
            if (i_sPath.EndsWith("\\"))
                return i_sPath;
            else
                return i_sPath + "\\";
        }

        public static void FolderXCopy(string i_sFrom, string i_sTo)
        {
            i_sFrom = CorrectPath(i_sFrom);
            i_sTo = CorrectPath(i_sTo);
            if (!Directory.Exists(i_sTo)) Directory.CreateDirectory(i_sTo);
            DirectoryInfo dFrom = new DirectoryInfo(i_sFrom);
            DirectoryInfo dTo = new DirectoryInfo(i_sTo);
            FileInfo[] a_f = dFrom.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < a_f.Length; i++)
            {
                a_f[i].CopyTo(i_sTo + a_f[i].Name, true);
            }
            DirectoryInfo[] a_d = dFrom.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < a_d.Length; i++)
            {
                FolderXCopy(a_d[i].FullName, i_sTo + a_d[i].Name);
            }
        }

        public static string GetValidIdentifier(string i_sIdentifier)
        {
            // Should start with an alpha, can contain only alpha, numbers and underscore
            if (i_sIdentifier.Length < 1) return null;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < i_sIdentifier.Length; i++)
            {
                if (sb.Length == 0)
                {
                    if (LOWERCASE.Contains(i_sIdentifier[i].ToString()) || UPPERCASE.Contains(i_sIdentifier[i].ToString()))
                    {
                        sb.Append(i_sIdentifier[i]);
                    }
                }
                else
                {
                    if (i_sIdentifier[i] == '_' || NUMBER.Contains(i_sIdentifier[i].ToString()) || LOWERCASE.Contains(i_sIdentifier[i].ToString()) || UPPERCASE.Contains(i_sIdentifier[i].ToString()))
                    {
                        sb.Append(i_sIdentifier[i]);
                    }
                }
            }
            if (sb.Length < 1) return null;
            return sb.ToString();
        }

        public static string GetValidNamespace(string i_sNamespace)
        {
            // Valid Identifiers seperated by .
            string[] a = i_sNamespace.Split('.');
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = GetValidIdentifier(a[i]);
                if (a[i] == null) return null;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                if (i > 0) sb.Append('.');
                sb.Append(a[i]);
            }
            return sb.ToString();
        }

        public static string GetValidPersonName(string i_sName)
        {
            // Valid Identifiers seperated by .
            string[] a = i_sName.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = GetValidIdentifier(a[i]);
                if (a[i] != null)
                {
                    if (sb.Length == 0) sb.Append(' ');
                    if (a[i].Length > 0)
                    {
                        sb.Append(a[i].Substring(0, 1).ToUpper() + a[i].Substring(1).ToLower());
                    }
                    else
                    {
                        sb.Append(a[i].ToUpper());
                    }
                }
            }
            return sb.ToString();
        }

        public static string GetValidEMailID(string i_sEmailID)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
                @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
                @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            Match match = Regex.Match(i_sEmailID.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return i_sEmailID.Trim();
            else
                return string.Empty;
        }

        public static string GetValidPhoneNumber(string i_sPhoneNumber)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < i_sPhoneNumber.Length; i++)
            {
                if (NUMBER.Contains(i_sPhoneNumber[i].ToString()))
                {
                    sb.Append(i_sPhoneNumber[i]);
                }
            }
            return sb.ToString();
        }

        public static string GetValidObjectReference(string i_sObjectReference)
        {
            const string sDefault = "NONE:NONE";
            string[] a = i_sObjectReference.Split(':');
            if (a.Length == 2)
            {
                //if (cClass.Get(a[0]) != null)
                //{
                return a[0] + ":NONE";
                //}
                //else
                //{
                //    return sDefault;
                //}
            }
            else
            {
                return sDefault;
            }
        }

        public static string GetDateTimeStamp()
        {
            DateTime dt = DateTime.UtcNow;
            return dt.ToString("yyyyMMddhhmmss");
        }

        public static string LimitLength(string i_sValue, int i_iMaxLength)
        {
            if (i_sValue.Length > i_iMaxLength)
            {
                return i_sValue.Substring(0, i_iMaxLength);
            }
            else
            {
                return i_sValue;
            }
        }

        public static enDataType GetPropertyDataTypeFromPrefix(string i_sName)
        {
            string sPrefix = string.Empty;
            for (int i = 0; i < i_sName.Length; i++)
            {
                if (LOWERCASE.Contains(i_sName[i].ToString()))
                {
                    sPrefix += i_sName[i];
                }
                else
                {
                    break;
                }
            }
            enDataType en = enDataType.String;
            switch (sPrefix)
            {
                case "b":
                    return enDataType.Boolean;
                case "i":
                    return enDataType.Int32;
                case "l":
                    return enDataType.Int64;
                case "s":
                    return enDataType.String;
                case "dt":
                    return enDataType.DateTime;
                case "m":
                    return enDataType.Currency;
                case "nm":
                    return enDataType.NameOfPerson;
                case "eml":
                    return enDataType.EMailID;
                case "ph":
                    return enDataType.Phone;
                case "url":
                    return enDataType.URL;
                case "obj":
                    return enDataType.ObjectReference;
                case "f":
                    return enDataType.Float;
            }
            return en;
        }

        public static string GetDescriptiveNameFromName(string i_sName)
        {
            string sName = string.Empty;
            bool bPrefix = true;
            for (int i = 0; i < i_sName.Length; i++)
            {
                if (LOWERCASE.Contains(i_sName[i].ToString()))
                {
                    if (!bPrefix)
                    {
                        sName += i_sName[i];
                    }
                }
                else
                {
                    if (bPrefix)
                    {
                        bPrefix = false;
                        sName += i_sName[i];
                    }
                    else
                    {
                        if (!UPPERCASE.Contains(sName[sName.Length - 1].ToString()))
                        {
                            sName += (" " + i_sName[i]);
                        }
                        else
                        {
                            sName += i_sName[i];
                        }
                    }
                }
            }
            return sName;
        }

        ///// <summary>
        ///// Get Image Of Specified Dimension
        ///// </summary>
        ///// <param name="i_sImageFile"></param>
        ///// <param name="i_iWidthPx"></param>
        ///// <param name="i_iHeightPx"></param>
        ///// <param name="i_bRecreate"></param>
        ///// <param name="o_sFileName"></param>
        ///// <returns></returns>
        //public static string GetImageOfSpecifiedDimension(string i_sImageFile, int i_iWidthPx, int i_iHeightPx, bool i_bRecreate, out string o_sFileName)
        //{
        //    string[] a = i_sImageFile.Split('.');
        //    string sFile = i_sImageFile.Replace("." + a[a.Length - 1], "_" + i_iWidthPx.ToString() + "x" + i_iHeightPx.ToString() + "." + a[a.Length - 1]);
        //    if (!i_bRecreate && File.Exists(sFile))
        //    {
        //        o_sFileName = (new FileInfo(sFile)).Name;
        //        return sFile;
        //    }
        //    Bitmap bmp = new Bitmap(i_sImageFile);
        //    //Image bmp1 = GetBestFit(i_iWidthPx, i_iHeightPx, (Image)bmp); // HardResizeImage(i_iWidthPx, i_iHeightPx, (Image)bmp); //ResizeBitmap(bmp, i_iWidthPx, i_iHeightPx);
        //    Image bmp1 = HardResizeImageWithBlackBacground(i_iWidthPx, i_iHeightPx, (Image)bmp);
        //    bmp.Dispose();
        //    if (File.Exists(sFile))
        //    {
        //        File.Delete(sFile);
        //    }
        //    bmp1.Save(sFile, ImageFormat.Jpeg);
        //    bmp1.Dispose();
        //    o_sFileName = (new FileInfo(sFile)).Name;
        //    return sFile;
        //}

        ///// <summary>
        ///// Resize and crop from top left corner
        ///// </summary>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_imgImage"></param>
        ///// <returns></returns>
        //public static Image GetBestFit(int i_iWidth, int i_iHeight, Image i_imgImage)
        //{
        //    int iSrcWidth = i_imgImage.Width;
        //    int iSrcHeight = i_imgImage.Height;
        //    int iDiff = iSrcWidth - iSrcHeight;
        //    double fFactor = 1.0 * i_iWidth / iSrcWidth;
        //    Image imgResized = null;
        //    //if (iDiff > 0)
        //    //{
        //    if (fFactor * iSrcHeight < i_iHeight)
        //    {
        //        // consider height
        //        fFactor = 1.0 * (i_iHeight + 20) / iSrcHeight;
        //        imgResized = ResizeImage(Convert.ToInt32(fFactor * iSrcWidth), i_iHeight, i_imgImage);
        //    }
        //    else
        //    {
        //        // consider width
        //        fFactor = 1.0 * (i_iWidth + 20) / iSrcWidth;
        //        imgResized = ResizeImage(i_iWidth, Convert.ToInt32(fFactor * iSrcHeight), i_imgImage);
        //    }
        //    //int iX = (imgResized.Width - i_iWidth) / 2;
        //    //int iY = (imgResized.Height - i_iHeight) / 2; ;
        //    //Image imgCropped = CropImage(imgResized, i_iHeight, i_iWidth, iX, iY);
        //    Image imgCropped = CropImage(imgResized, i_iHeight, i_iWidth);
        //    return imgCropped;
        //    //}
        //    //else if (iDiff < 0)
        //    //{

        //    //}
        //    //else
        //    //{

        //    //}
        //}

        ///// <summary>
        ///// Resize without aspect
        ///// </summary>
        ///// <param name="i_bmpSrc"></param>
        ///// <param name="i_iWidthPx"></param>
        ///// <param name="i_iHeightPx"></param>
        ///// <returns></returns>
        //private static Bitmap ResizeBitmapWithoutAspect(Bitmap i_bmpSrc, int i_iWidthPx, int i_iHeightPx)
        //{
        //    Bitmap result = new Bitmap(i_iWidthPx, i_iHeightPx);
        //    using (Graphics g = Graphics.FromImage(result))
        //    {
        //        g.DrawImage(i_bmpSrc, 0, 0, i_iWidthPx, i_iHeightPx);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// Overload for crop that default starts top left of the image.
        ///// </summary>
        ///// <param name="i_imgImage"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_iWidth"></param>
        ///// <returns></returns>
        //private static Image CropImage(Image i_imgImage, int i_iHeight, int i_iWidth)
        //{
        //    return CropImage(i_imgImage, i_iHeight, i_iWidth, 0, 0);
        //}

        ///// <summary>
        ///// The crop image sub
        ///// </summary>
        ///// <param name="i_imgImage"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iStartAtX"></param>
        ///// <param name="i_iStartAtY"></param>
        ///// <returns></returns>
        //public static Image CropImage(Image i_imgImage, int i_iHeight, int i_iWidth, int i_iStartAtX, int i_iStartAtY)
        //{
        //    Image outimage;
        //    MemoryStream mm = null;
        //    try
        //    {
        //        //check the image height against our desired image height
        //        if (i_imgImage.Height < i_iHeight)
        //        {
        //            i_iHeight = i_imgImage.Height;
        //        }

        //        if (i_imgImage.Width < i_iWidth)
        //        {
        //            i_iWidth = i_imgImage.Width;
        //        }

        //        //create a bitmap window for cropping
        //        Bitmap bmPhoto = new Bitmap(i_iWidth, i_iHeight, PixelFormat.Format24bppRgb);
        //        bmPhoto.SetResolution(72, 72);

        //        //create a new graphics object from our image and set properties
        //        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //        grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
        //        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        //now do the crop
        //        grPhoto.DrawImage(i_imgImage, new Rectangle(0, 0, i_iWidth, i_iHeight), i_iStartAtX, i_iStartAtY, i_iWidth, i_iHeight, GraphicsUnit.Pixel);

        //        // Save out to memory and get an image from it to send back out the method.
        //        mm = new MemoryStream();
        //        bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        i_imgImage.Dispose();
        //        bmPhoto.Dispose();
        //        grPhoto.Dispose();
        //        outimage = Image.FromStream(mm);

        //        return outimage;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error cropping image, the error was: " + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        ///// </summary>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_imgImage"></param>
        ///// <returns></returns>
        //public static Image HardResizeImage(int i_iWidth, int i_iHeight, Image i_imgImage)
        //{
        //    int width = i_imgImage.Width;
        //    int height = i_imgImage.Height;
        //    Image resized = null;
        //    if (i_iWidth > i_iHeight)
        //    {
        //        resized = ResizeImage(i_iWidth, i_iWidth, i_imgImage);
        //    }
        //    else
        //    {
        //        resized = ResizeImage(i_iHeight, i_iHeight, i_imgImage);
        //    }
        //    Image output = CropImage(resized, i_iHeight, i_iWidth);
        //    //return the original resized image
        //    return output;
        //}

        ///// <summary>
        ///// Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        ///// The returned image will match the given dimensions, the excess area will be black
        ///// </summary>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_imgImage"></param>
        ///// <returns></returns>
        //public static Image HardResizeImageWithBlackBacground(int i_iWidth, int i_iHeight, Image i_imgImage)
        //{
        //    int width = i_imgImage.Width;
        //    int height = i_imgImage.Height;
        //    Image resized = null;
        //    resized = ResizeImage(i_iWidth, i_iHeight, i_imgImage);
        //    /*
        //    if (i_iWidth > i_iHeight)
        //    {
        //        resized = ResizeImage(i_iWidth, i_iWidth, i_imgImage);

        //    }
        //    else
        //    {
        //        resized = ResizeImage(i_iHeight, i_iHeight, i_imgImage);
        //    }
        //    */
        //    Image output = CropImage(resized, i_iHeight, i_iWidth);
        //    //return the original resized image
        //    Bitmap o_bmp = new Bitmap(i_iWidth, i_iHeight);
        //    Graphics g = Graphics.FromImage(o_bmp);
        //    int x = Convert.ToInt32((i_iWidth - output.Width) / 2.0);
        //    int y = Convert.ToInt32((i_iHeight - output.Height) / 2.0);
        //    g.DrawImage(output, new Rectangle(x, y, output.Width, output.Height), new Rectangle(0, 0, output.Width, output.Height), GraphicsUnit.Pixel);
        //    g.Dispose();
        //    return o_bmp;
        //}
        ///// <summary>
        ///// Image resizing
        ///// </summary>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_imgImage"></param>
        ///// <returns></returns>
        //public static Image ResizeImage(int i_iWidth, int i_iHeight, Image i_imgImage)
        //{
        //    int width = i_imgImage.Width;
        //    int height = i_imgImage.Height;
        //    //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
        //    //from displaying as the thumbnail image later, in other words, we want a clean
        //    //resize, not a grainy one.
        //    i_imgImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
        //    i_imgImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);

        //    float ratio = 0;
        //    if (width > height)
        //    {
        //        ratio = (float)width / (float)height;
        //        width = i_iWidth;
        //        height = Convert.ToInt32(Math.Round((float)width / ratio));
        //    }
        //    else
        //    {
        //        ratio = (float)height / (float)width;
        //        height = i_iHeight;
        //        width = Convert.ToInt32(Math.Round((float)height / ratio));
        //    }

        //    //return the resized image
        //    return i_imgImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
        //    //return the original resized image
        //    //return i_imgImage;
        //}

        ///// <summary>
        ///// Image resizing
        ///// </summary>
        ///// <param name="i_iWidth"></param>
        ///// <param name="i_iHeight"></param>
        ///// <param name="i_imgImage"></param>
        ///// <returns></returns>
        //public static Image ResizeImageIfBigger(int i_iWidth, int i_iHeight, Image i_imgImage)
        //{
        //    int width = i_imgImage.Width;
        //    int height = i_imgImage.Height;
        //    if (width > i_iWidth || height > i_iHeight)
        //    {
        //        //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
        //        //from displaying as the thumbnail image later, in other words, we want a clean
        //        //resize, not a grainy one.
        //        i_imgImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
        //        i_imgImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);

        //        float ratio = 0;
        //        if (width > height)
        //        {
        //            ratio = (float)width / (float)height;
        //            width = i_iWidth;
        //            height = Convert.ToInt32(Math.Round((float)width / ratio));
        //        }
        //        else
        //        {
        //            ratio = (float)height / (float)width;
        //            height = i_iHeight;
        //            width = Convert.ToInt32(Math.Round((float)height / ratio));
        //        }

        //        //return the resized image
        //        return i_imgImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
        //    }
        //    //return the original resized image
        //    return i_imgImage;
        //}
    }
}