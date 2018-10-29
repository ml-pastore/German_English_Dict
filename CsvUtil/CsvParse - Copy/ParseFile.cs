using System;

public class ParseFIle
{

	public string fileName {set; get;}

	public virtual void CleanTheFile(){;}

}

public class ParseFIleNoun : ParseFIle
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Noun file: {fileName}");
	;}

}
public class ParseFIleAdj : ParseFIle
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Adj file: {fileName}");
	;}	
	
}
public class ParseFIleAdv : ParseFIle
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Adv file: {fileName}");
	;}	
}
public class ParseFIleVerb : ParseFIle
{
	public override void CleanTheFile()
	{
		Console.WriteLine($"Parsing Verb file: {fileName}");
	;}	
}
