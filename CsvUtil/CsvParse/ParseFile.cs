using System;

public class ParseFile
{

	public string fileName {set; get;}

	public virtual void CleanTheFile(){;}

}

public class ParseFileNoun : ParseFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Noun file: {fileName}");
	;}

}
public class ParseFileAdj : ParseFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Adj file: {fileName}");
	;}	
	
}
public class ParseFileAdv : ParseFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Adv file: {fileName}");
	;}	
}
public class ParseFileVerb : ParseFile
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Verb file: {fileName}");
	;}	
}
