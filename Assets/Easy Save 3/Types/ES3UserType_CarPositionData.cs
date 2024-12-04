using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("position", "rotation", "time")]
	public class ES3UserType_CarPositionData : ES3Type
	{
		public static ES3Type Instance = null;

		public ES3UserType_CarPositionData() : base(typeof(CarPositionData)){ Instance = this; priority = 1;}


		public override void Write(object obj, ES3Writer writer)
		{
			var instance = (CarPositionData)obj;
			
			writer.WriteProperty("position", instance.position, ES3Type_Vector3.Instance);
			writer.WriteProperty("rotation", instance.rotation, ES3Type_Quaternion.Instance);
			writer.WriteProperty("time", instance.time, ES3Type_float.Instance);
		}

		public override object Read<T>(ES3Reader reader)
		{
			var instance = new CarPositionData();
			string propertyName;
			while((propertyName = reader.ReadPropertyName()) != null)
			{
				switch(propertyName)
				{
					
					case "position":
						instance.position = reader.Read<UnityEngine.Vector3>(ES3Type_Vector3.Instance);
						break;
					case "rotation":
						instance.rotation = reader.Read<UnityEngine.Quaternion>(ES3Type_Quaternion.Instance);
						break;
					case "time":
						instance.time = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
			return instance;
		}
	}


	public class ES3UserType_CarPositionDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_CarPositionDataArray() : base(typeof(CarPositionData[]), ES3UserType_CarPositionData.Instance)
		{
			Instance = this;
		}
	}
}