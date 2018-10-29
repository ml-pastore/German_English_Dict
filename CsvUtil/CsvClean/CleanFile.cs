using System;
using System.IO;

public class CleanFile 
{
	private bool disposed = false;
	public string fileName {set; get;}
	private FileStream _fs;


	public virtual void CleanTheFile(){;}

	public void CleanChars()
	{
		Console.WriteLine($"Cleaning characters");

	}

}

public class CleanFileNoun : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Noun file: {fileName}");

		base.CleanChars();

	;}



}
public class CleanFileAdj : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Adj file: {fileName}");
	;}	
	
}
public class CleanFileAdv : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Adv file: {fileName}");
	;}	
}
public class CleanFileVerb : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Verb file: {fileName}");
	;}	
}
