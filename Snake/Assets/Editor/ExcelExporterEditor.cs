#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

public struct CellInfo
{
	public string Type;
	public string Name;
	public string Desc;
}

public class ExcelMD5Info
{
	public Dictionary<string, string> fileMD5 = new Dictionary<string, string>();

	public string Get(string fileName)
	{
		string md5 = "";
		this.fileMD5.TryGetValue(fileName, out md5);
		return md5;
	}

	public void Add(string fileName, string md5)
	{
		this.fileMD5[fileName] = md5;
	}
}

public class ExcelExporterEditor : EditorWindow
{
	[MenuItem("Tools/导入Excel数据")]
	private static void ShowWindow()
	{
		//GetWindow(typeof(ExcelExporterEditor));
	}

	private const string ExcelPath = "./Excel";
	//private const string ServerConfigPath = "./Assets/_GameCommon/ExcelText";

	private bool isClient;

	private ExcelMD5Info md5Info;

	// Update is called once per frame
	private void OnGUI()
	{
		try
		{
			const string clientPath = "./Assets/_GameCommon/ExcelText";

			if (GUILayout.Button("导入Excel配置"))
			{
				this.isClient = true;

				ExportAll(clientPath);

				ExportAllClass(@"./Assets/_GameCommon/ExcelConfig", "namespace GameMain.ExcelData\n{\n");
				//ExportAllClass(@"./Assets/Hotfix/Entity/Config", "namespace Test\n{\n");

				Debug.Log($"导入Excel配置完成!");
			}

			//if (GUILayout.Button("导出服务端配置"))
			//{
			//	this.isClient = false;

			//	ExportAll(ServerConfigPath);

			//	ExportAllClass(@"../Server/Model/Entity/Config", "namespace Test\n{\n");

			//	Log.Info($"导出服务端配置完成!");
			//}
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	private void ExportAllClass(string exportDir, string csHead)
	{
		foreach (string filePath in Directory.GetFiles(ExcelPath))
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			if (Path.GetFileName(filePath).StartsWith("~"))
			{
				continue;
			}

			ExportClass(filePath, exportDir, csHead);
			Debug.Log($"生成{Path.GetFileName(filePath)}类");
		}
		AssetDatabase.Refresh();
	}

	private void ExportClass(string fileName, string exportDir, string csHead)
	{
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}

		string protoName = Path.GetFileNameWithoutExtension(fileName);

		string exportPath = Path.Combine(exportDir, $"{protoName}.cs");

		if (File.Exists(exportDir))
		{
			File.Delete(exportDir);
		}

		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			StringBuilder sb = new StringBuilder();
			ISheet sheet = xssfWorkbook.GetSheetAt(0);
			sb.Append(csHead);
			//sb.Append("\n\t[MongoDB.Bson.Serialization.Attributes.BsonIgnoreExtraElements]");
			sb.Append("\n\t[System.Serializable]");

			//sb.Append($"\t[Config({GetCellString(sheet, 0, 0)})]\n");
			//sb.Append($"\tpublic partial class {protoName}Category : ACategory<{protoName}>\n");
			//sb.Append("\t{\n");
			//sb.Append("\t}\n\n");

			var fileDesc = GetCellString(sheet, 2, 0);
			if (string.IsNullOrEmpty(fileDesc) == false)
			{
				sb.Append($"\n\t/// <summary>{fileDesc}</summary>\n");
			}

			var codeName = GetCellString(sheet, 3, 0);
			codeName = string.IsNullOrEmpty(codeName) ? fileName : codeName;

			var fileType = GetCellString(sheet, 4, 0);
			fileType = string.IsNullOrEmpty(fileType) ? "class" : fileType;

			if (protoName.StartsWith("#"))
			{
				protoName = protoName.Substring(1);
			}

			sb.Append($"\n\tpublic {fileType} {protoName}: IConfig");
			sb.Append("\n\t{");
			//sb.Append("\n\t\t[System.NonSerialized]");
			//sb.Append("\n\t\tpublic bool isDisposed;");
			//sb.Append("\n\t\tpublic void Dispose()");
			//sb.Append("\n\t\t{");
			//sb.Append("\n\t\t\tif (this.isDisposed)");
			//sb.Append("\n\t\t\t\treturn;");
			//sb.Append("\n\t\t\tthis.Dispose();");
			//sb.Append("\n\t\t}");
			//sb.Append("\t\tpublic MongoDB.Bson.ObjectId Id { get; set; }\n");

			int cellCount = sheet.GetRow(3).LastCellNum;
			List<string> temp = new List<string>();

			for (int i = 2; i < cellCount; i++)
			{
				string fieldDesc = GetCellString(sheet, 2, i);
				if (fieldDesc.StartsWith("#"))
				{
					continue;
				}
				// s开头表示这个字段是服务端专用
				if (fieldDesc.StartsWith("s") && this.isClient)
				{
					continue;
				}

				string fieldName = GetCellString(sheet, 3, i);
				//if (fieldName == "Id" || fieldName == "_id")
				//{
				//	continue;
				//}

				if (fieldDesc != "")
				{
					sb.Append($"\n\t\t/// <summary>{fieldDesc}</summary>\n");
				}

				string fieldType = GetCellString(sheet, 4, i);
				if (fieldType == "" || fieldName == "")
				{
					continue;
				}
				temp.Add(fieldName);
				sb.Append($"\t\tpublic {fieldType} {fieldName};\n");
			}
			sb.Append("\n");
			sb.Append("\t\tpublic override string ToString()\n");
			sb.Append("\t\t{\n");
			sb.Append("\t\t\treturn $\" ");
			foreach (var n in temp)
			{
				sb.Append($",{n}:{{this.{n}}} ");
			}
			sb.Append("\";\n\t\t}");
			temp.Clear();

			sb.Append("\n\t}\n");
			sb.Append("}\n");

			sw.Write(sb.ToString());
		}
	}

	private void ExportAll(string exportDir)
	{
		string md5File = Path.Combine(ExcelPath, "md5.txt");
		if (!File.Exists(md5File))
		{
			this.md5Info = new ExcelMD5Info();
		}
		else
		{
			this.md5Info = SerializeHelper.FromJson<ExcelMD5Info>(File.ReadAllText(md5File));
		}

		foreach (string filePath in Directory.GetFiles(ExcelPath))
		{
			if (Path.GetExtension(filePath) != ".xlsx")
			{
				continue;
			}
			string fileName = Path.GetFileName(filePath);

			if (fileName.StartsWith("#"))
			{
				continue;
			}

			string oldMD5 = this.md5Info.Get(fileName);
			string md5 = Md5Helper.FileMD5(filePath);
			this.md5Info.Add(fileName, md5);
			if (md5 == oldMD5)
			{
				continue;
			}

			Export(filePath, exportDir);
		}

		//File.WriteAllText(md5File, this.md5Info.ToJson());
		File.WriteAllText(md5File, SerializeHelper.ToJson(this.md5Info));

		Debug.Log("所有表导表完成");
		AssetDatabase.Refresh();
	}

	private void Export(string fileName, string exportDir)
	{
		//if (fileName.StartsWith("#"))
		//{
		//	fileName = fileName.Substring(2);
		//}
		XSSFWorkbook xssfWorkbook;
		using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			xssfWorkbook = new XSSFWorkbook(file);
		}
		string protoName = Path.GetFileNameWithoutExtension(fileName);
		Debug.Log($"{protoName}导表开始");
		string exportPath = Path.Combine(exportDir, $"{protoName}.txt");
		using (FileStream txt = new FileStream(exportPath, FileMode.Create))
		using (StreamWriter sw = new StreamWriter(txt))
		{
			for (int i = 0; i < xssfWorkbook.NumberOfSheets; ++i)
			{
				ISheet sheet = xssfWorkbook.GetSheetAt(i);
				ExportSheet(sheet, sw);
			}
		}
		Debug.Log($"{protoName}导表完成");
	}

	private void ExportSheet(ISheet sheet, StreamWriter sw)
	{
		int cellCount = sheet.GetRow(3).LastCellNum;

		CellInfo[] cellInfos = new CellInfo[cellCount];

		for (int i = 2; i < cellCount; i++)
		{
			string fieldDesc = GetCellString(sheet, 2, i);
			string fieldName = GetCellString(sheet, 3, i);
			string fieldType = GetCellString(sheet, 4, i);
			cellInfos[i] = new CellInfo() { Name = fieldName, Type = fieldType, Desc = fieldDesc };
		}

		for (int i = 5; i <= sheet.LastRowNum; ++i)
		{
			if (GetCellString(sheet, i, 2) == "")
			{
				continue;
			}
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			IRow row = sheet.GetRow(i);
			for (int j = 2; j < cellCount; ++j)
			{
				string desc = cellInfos[j].Desc.ToLower();

				if (desc.StartsWith("#"))
				{
					continue;
				}

				// s开头表示这个字段是服务端专用
				if (desc.StartsWith("s") && this.isClient)
				{
					continue;
				}

				// c开头表示这个字段是客户端专用
				if (desc.StartsWith("c") && !this.isClient)
				{
					continue;
				}

				string fieldValue = GetCellString(row, j);
				if (fieldValue == "")
				{
					throw new Exception($"sheet: {sheet.SheetName} 中有空白字段 {i},{j}");
				}

				if (j > 2)
				{
					sb.Append(",");
				}

				string fieldName = cellInfos[j].Name;

				if (fieldName == "Id" || fieldName == "_id")
				{
					if (this.isClient)
					{
						fieldName = "Id";
					}
					else
					{
						fieldName = "_id";
					}
				}

				string fieldType = cellInfos[j].Type;
				sb.Append($"\"{fieldName}\":{Convert(fieldType, fieldValue)}");
			}
			sb.Append("}");
			sw.WriteLine(sb.ToString());
		}
	}

	private static string Convert(string type, string value)
	{
		switch (type)
		{
			case "int[]":
			case "int32[]":
			case "long[]":
				return $"[{value}]";
			case "string[]":
				{
					var temp = value.Split(',');
					StringBuilder sb = new StringBuilder();
					foreach (var item in temp)
					{
						sb.Append($"\"{item}\"");
					}
					return $"[{sb.ToString()}]";
				}
			case "int":
			case "int32":
			case "int64":
			case "long":
			case "float":
			case "double":
				return value;
			case "string":
				return $"\"{value}\"";
			case "bool":
				return $"{value.ToLower()}";
			default:
				if (type.Contains("[]"))
				{
					Debug.LogError($"{type} is a enum?");
					return $"[{value}]";
					// throw new Exception($"不支持此类型: {type}");
				}
				else
				{
					Debug.LogError($"{type} is a enum?");
					return value;
				}
				//throw new Exception($"不支持此类型: {type}");
		}
	}

	private static string GetCellString(ISheet sheet, int i, int j)
	{
		//var temp = sheet.GetRow(i)?.GetCell(j);
		//tem
		//if (temp.HasValue)
		//{
		return sheet.GetRow(i)?.GetCell(j)?.ToString() ?? "";
		//}
		//else
		//{
		//	return sheet.GetRow(i)?.GetCell(j)?.StringCellValue ?? "";
		//}
	}

	private static string GetCellString(IRow row, int i)
	{
		return row?.GetCell(i)?.ToString() ?? "";
	}

	private static string GetCellString(ICell cell)
	{
		return cell?.ToString() ?? "";
	}
}
#endif

