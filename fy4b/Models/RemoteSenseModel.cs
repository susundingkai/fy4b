using System;
namespace fy4b.Models
{
	public class RemoteSenseModel
	{
		public DateTime Date {get;set;}
		public ImageSource RemoteImageSource { get; set; }
		public RemoteSenseModel(string DirectPath)
		{
			Date = DateTime.Now;
            RemoteImageSource = ImageSource.FromFile("/users/ssdk/0629.png");
		}
	}
}

