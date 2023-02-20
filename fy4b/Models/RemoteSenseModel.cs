using System;
using System.Collections.ObjectModel;

namespace fy4b.Models
{
	public class RemoteSenseModel
	{
		public DateTime Date {get;set;}
		public string[] ChannelPaths { get;set;}
		public ObservableCollection<RemoteSenseImageModel> Images { get;set;}
        public RemoteSenseModel(string DirectPath)
		{
            Images=new();
            //20221110050000
			string dateString= DirectPath.Replace("\\","/").Split("/").Last();
            long dateInt =Convert.ToInt64(dateString);
			dateInt /= 10000;
			int hour= (int)(dateInt % 100);
			dateInt/=100;
			int day=(int)(dateInt % 100);
			dateInt/= 100;
			int month=(int)(dateInt % 100);
			dateInt/= 100;
			int year=(int)dateInt;
            Date=new DateTime(year,month,day,hour,0,0);
			ChannelPaths = Directory.GetFiles(DirectPath);
			for(int i = 0; i < ChannelPaths.Length; i++)
			{
				Images.Add(new RemoteSenseImageModel(i, ChannelPaths[i],Date));
			}
		}
	}
}

