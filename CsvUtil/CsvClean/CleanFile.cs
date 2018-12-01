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
	private string _validChars = "";

	public string ValidChars{
		set{
			_validChars = value;
			 _goodChars = new Regex($"^[{_validChars}]+$"); // default

		}
		get{
			return _validChars;
		}

	}
	
	private Regex _goodChars;

	public CleanFile()
	{
		ValidChars = "a-zA-Z0-9äöüÄÖÜßß|' ,-_\"";
	}
	public virtual void CleanTheFile(){;}

	public bool LineIsValid(string ln)
	{

		return _goodChars.IsMatch(ln, 0);
		throw new NotImplementedException("Not implemented!");

	}

	private string _fileEncode = "iso-8859-1";
	public void CleanChars()
	{

		ILog.Write($"Cleaning characters");
		ILog.Write($"Input: {FileName}");
		ILog.Write($"Valid: {_goodChars}");
		
		string[] lns = File.ReadAllLines(FileName, Encoding.GetEncoding(_fileEncode));

	
		int lnCtr = 0;
		int numInvalid = 0;
		foreach(string ln in lns)
		{

			if(! LineIsValid(ln))
			{
				numInvalid++;

				foreach(char c in ln.ToCharArray())
				{
					if(! LineIsValid(c.ToString()))
					{
						lns[lnCtr] = lns[lnCtr].Replace(c.ToString(),"");
					}

				}
				
				ILog.Write($"Ln: {lnCtr.ToString()}");
				ILog.Write($"Orig: {ln.ToString()}");
				ILog.Write($"New: {lns[lnCtr].ToString()}");

			}	

			lnCtr++;
		} // each line

		if(numInvalid > 0)
		{
			string outFile = Path.GetFileName(FileName);
			outFile = $"_out\\{outFile}";
			ILog.Write($"Output: {outFile}");
			File.WriteAllLines(outFile, lns, Encoding.GetEncoding(_fileEncode));
		}
		else
			ILog.Write($"All lines valid!");

	} // CleanChars()

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
		ILog.Write($"Cleaning Noun file: {FileName}");
		ILog.Write($"Commencing...");
		base.CleanChars();
	;}	
	
}
public class CleanFileAdv : CleanFile
{
	public override void CleanTheFile()
	{
		throw new NotImplementedException("Not implemented!");
		Console.WriteLine($"Cleaning Adv file: {FileName}");
	;}	
}
public class CleanFileVerb : CleanFile
{
	public override void CleanTheFile()
	{
		throw new NotImplementedException("Not implemented!");
		Console.WriteLine($"Cleaning Verb file: {FileName}");
	;}	
}
