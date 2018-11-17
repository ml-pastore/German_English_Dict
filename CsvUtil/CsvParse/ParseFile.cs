using System;
using System.IO;
using System.Collections.Generic;

using CsvHelper;
using System.Text;


public class ParseFile
{
	public ILogger ILog {set; get;}

	public string FileName {set; get;}

	public string FileRawHeader {set; get;}

	public virtual void ParseTheFile(){;}

 	public virtual string GetHeader(){return "";}
	
	private string _fileEncode = "iso-8859-1";

	public IEnumerable<T> ParseTheFile<T>(T rec)
	{
		
		string absPath = Path.GetFullPath(FileName);
		IEnumerable<T> ret = new List<T>();

		StreamReader reader = new StreamReader(absPath,  Encoding.GetEncoding(_fileEncode), true);
		var csv = new CsvReader(reader);
        csv.Configuration.HasHeaderRecord = true;
		ret = csv.GetRecords<T>();

		return ret;

	}

}

public class ParseFileNoun : ParseFile
{

	public ParseFileNoun()
	{
		FileRawHeader = "Noun_English,Plural_English,Noun_German,Gender_German,Plural_German,Parts_German,Idiom_English,Idiom_German,Categories";
	}

	public override void ParseTheFile()
	{
		Console.WriteLine($"Parsing Noun file: {FileName}");
		ILog.Write($"Parsing Noun file: {FileName}");
		
		var rec = new
		{
    		Noun_English = string.Empty,
			Plural_English = string.Empty,
			Noun_German = string.Empty,
			Gender_German = string.Empty,
			Plural_German = string.Empty,
			Parts_German = string.Empty,
			Idiom_English = string.Empty,
			Idiom_German = string.Empty,
			Categories = string.Empty
    	};

		var recs = base.ParseTheFile(rec);

		// foreach(var r in recs)
		// 	Console.Write(r.Noun_German + ",");


	}
}

public class ParseFileAdj : ParseFile
{

	public ParseFileAdj()
	{
		FileRawHeader = "Adj_English,Adj_German,Parts_German,Idiom_English,Idiom_German,Categories";
	}

	public override void ParseTheFile()
	{
	
		Console.WriteLine($"Parsing Adj file: {FileName}");

		var rec = new
		{
    		Adj_English = string.Empty,
			Adj_German = string.Empty,
			Parts_German = string.Empty,
			Idiom_English = string.Empty,
			Idiom_German = string.Empty,
			Categories = string.Empty,
		};

		var recs = base.ParseTheFile(rec);

		// foreach(var r in recs)
		// 	Console.Write(r.Adj_German + ",");


	}	
	
}
public class ParseFileAdv : ParseFile
{
	public override void ParseTheFile()
	{
		throw new NotImplementedException("Not implemented!");
		Console.WriteLine($"Parsing Adv file: {FileName}");
	;}	
}
public class ParseFileVerb : ParseFile
{
	public override void ParseTheFile()
	{
		throw new NotImplementedException("Not implemented!");
		Console.WriteLine($"Parsing Verb file: {FileName}");
	;}	
}
