/* This file is automatically generated by WebApiProxy Parser Generator */

namespace WebApiProxy.Clients.CSharp
{
	using System.Linq;
	using WebApiProxy.Core.Infrastructure;

    public partial class CSharpGenerator
    {
        private System.Text.StringBuilder __sb;

        private void Write(string text) {
            __sb.Append(text);
        }

        private void WriteLine(string text) {
            __sb.AppendLine(text);
        }

        private string transformText()
        {
            __sb = new System.Text.StringBuilder();
__sb.Append(@"

//To be implemented yet. host: ");
__sb.Append( this.Metadata.Host );

            return __sb.ToString();
        }
    }
}
