﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web;
using BrightcoveSDK.JSON;

namespace BrightcoveSDK.Media
{	
	/// <summary>
	/// This object represents metadata about an image file in your account. 
	/// Images are associated with videos as thumbnail images or video still images. 
	/// An image can be a JPEG, GIF, or PNG-formatted image.
	/// </summary>
	[DataContract]
	public class BCImage : BCObject, IComparable<BCImage>
	{	
		/// <summary>
		/// A number that uniquely identifies this Image, assigned by Brightcove when the Image is created.
		/// </summary>
		[DataMember]
		public long id { get; set; }

		/// <summary>
		/// A user-specified id that uniquely identifies this Image. ReferenceID can be used as a foreign-key to identify this Image in another system. 
		/// </summary> 
		[DataMember]
		public string referenceId { get; set; }

		[DataMember(Name="type")]
		private string imageType { get; set; }
		
		/// <summary>
		/// Either THUMBNAIL or VIDEO_STILL. The type is writable and required when you create an Image; it cannot subsequently be changed.
		/// </summary> 
		public ImageTypeEnum type {
			get {
				return (string.IsNullOrEmpty(imageType)) ? ImageTypeEnum.VIDEO_STILL : (ImageTypeEnum)Enum.Parse(typeof(ImageTypeEnum), imageType, true);
			}
			set {
				imageType = value.ToString();
			}
		}

		/// <summary>
		/// The URL of a remote image file. This property is required if you are not uploading a file for the image asset.
		/// </summary> 
		[DataMember]
		public string remoteUrl { get; set; }
		
		/// <summary>
		/// The name of the asset, which will be displayed in the Media module.
		/// </summary> 
		[DataMember]
		public string displayName { get; set; }

		#region IComparable Comparators

		public int CompareTo(BCImage other) {
			return id.CompareTo(other.id);
		}

		#endregion

	}

	public static class BCImageExtensions {

		#region Extension Methods

		public static string ToJSON(this BCImage image) {

			Builder jsonImage = new Builder(",", "{", "}");

			//Build Image in JSON 
			jsonImage.AppendField("displayName", image.displayName);
			
			if(!string.IsNullOrEmpty(image.referenceId))
				jsonImage.AppendField("referenceId", image.referenceId);
			
			if(!string.IsNullOrEmpty(image.remoteUrl))
				jsonImage.AppendField("remoteUrl", image.remoteUrl);
			
			jsonImage.AppendObject("type", image.type.ToString());
			
			return jsonImage.ToString();
		}

		#endregion
	}
}
