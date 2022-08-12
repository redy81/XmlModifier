using System.Xml;

namespace XmlModifier
{
    internal class Program
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "XmlModifier             v1.0            F.Branchetti" );

            if (args.Length < 3)
            {
                Console.WriteLine( "Not enough parameters!" );
                Environment.Exit( 1 );
            }

            if (!File.Exists( args[0] ))
            {
                Console.WriteLine( "Input file does not exist!" );
                Environment.Exit( 2 );
            }

            string filePath = args[0];
            string elementTag = args[1];
            string newText = args[2];

            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlTargetNodes;

            try
            {
                xmlDoc.Load( filePath );
            }
            catch (Exception)
            {
                Console.WriteLine( "Invalid XML structure!" );
                Environment.Exit( 3 );
                return; // Makes the compiler happy
            }

            // Find the path
            try
            {
                xmlTargetNodes = xmlDoc.SelectNodes( elementTag );
            }
            catch (Exception)
            {
                Console.WriteLine( "Invalid Xpath!" );
                Environment.Exit( 4 );
                return; // Makes the compiler happy
            }

            Console.WriteLine( $"> Found {xmlTargetNodes.Count} nodes @ {elementTag}" );

            foreach (XmlElement el in xmlTargetNodes)
            {
                Console.WriteLine( $"- {el.Name}" );
                el.InnerText = newText;
            }

            try
            {
                File.Copy( filePath, filePath + ".bak" );

                xmlDoc.Save( filePath );
            }
            catch (Exception ex)
            {
                Console.WriteLine( "Cannot save back to the file!" );
                Environment.Exit( 5 );
            }

            Environment.Exit( 0 );
        }
    }
}