using System;

public class CleanFile
{

	public string fileName {set; get;}

	public virtual void CleanTheFile(){;}

}

public class CleanFileNoun : CleanFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Cleaning Noun file: {fileName}");
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
