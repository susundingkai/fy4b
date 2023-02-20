using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu;
using Emgu.CV.Structure;
using Emgu.CV.Ocl;
using System.Drawing;

namespace fy4b.Models
{
    public class RemoteSenseImageModel
    {
        public int ChannelIndex { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
        public ImageSource Source { get
            {
                // Bitmap
                Image<Gray, Byte> img1 = new Image<Gray, Byte>(ImagePath);// path can be absolute or relative
                var filePath = Path.Combine(Path.GetTempPath(),"fy4b.png");

                img1.Save(filePath);
                return ImageSource.FromFile(filePath);
            } }
        public RemoteSenseImageModel(int _channelIndex, string _ImagePath,DateTime _date) {
            ChannelIndex = _channelIndex;
            ImagePath = _ImagePath;
            Date = _date;
        }

    }
}
