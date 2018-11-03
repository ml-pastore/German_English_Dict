using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

public class CleanFile 
{
	private bool _disposed = false;
	public string FileName {set; get;}
	public ILogger ILog {set; get;}
	
	//^[^<>]*$
	private Regex _goodChars = new Regex(@"^[a-zA-Z0-9äöüÄÖÜßß|' ,-_]+$");

	public virtual void CleanTheFile(){;}

	public void CleanChars()
	{

		ILog.Write($"Cleaning characters");
		ILog.Write($"Valid: {_goodChars}");
		
		string[] lns = File.ReadAllLines(FileName, Encoding.GetEncoding("iso-8859-1"));

		string[] badLns = lns
			.Select(x => x.Replace("\"",""))
			.Where(ln => ! _goodChars.IsMatch(ln, 0)).ToArray();
			
		Console.WriteLine(badLns.Count());

	}

}

public class CleanFileNoun : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Noun file: {FileName}");
		ILog.Write($"Cleaning Noun file: {FileName}");
		ILog.Write($"Commencing...");



		base.CleanChars();

	;}



}
public class CleanFileAdj : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Adj file: {FileName}");
	;}	
	
}
public class CleanFileAdv : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Adv file: {FileName}");
	;}	
}
public class CleanFileVerb : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Verb file: {FileName}");
	;}	
}
