using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }

            // Create a listener.
            HttpListener listener = new HttpListener();

            // Trap Ctrl-C and exit 
            Console.CancelKeyPress += delegate
            {
                listener.Stop();
                System.Environment.Exit(0);
            };

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                Console.WriteLine("Test 1");
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
                Console.WriteLine("Test 2");
            }

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                Header header = new Header(request);

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                header.printAll();
                header.print(HttpRequestHeader.ContentType); //MIME
                header.print(HttpRequestHeader.Cookie); //Cookie
                header.print(HttpRequestHeader.UserAgent); // L’agent utilisateur demandeur
                header.print(HttpRequestHeader.AcceptEncoding); //  les encodages de contenu admis pour la réponse
                header.print(HttpRequestHeader.Authorization); // les informations d’identification que le client doit présenter pour s’authentifier auprès du serveur
                header.print(HttpRequestHeader.ContentLanguage); // langages naturels préférés pour la réponse
                header.print(HttpRequestHeader.AcceptCharset); //les jeux de caractères admis pour la réponse
                header.print(HttpRequestHeader.AcceptCharset); // le jeu de méthodes HTTP pris en chargeConsole.WriteLine($"Received request for {request.Url}");
                Console.WriteLine(documentContents);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // Construct a response.
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            // Httplistener neither stop ...
            // listener.Stop();
        }
    }

    public class Header
    {
        private HttpListenerRequest listenerRequest;

        public Header(HttpListenerRequest req)
        {
            this.listenerRequest = req;
        }

        public void printAll()
        {
            foreach (string str in Enum.GetNames(typeof(HttpRequestHeader)))
            {
                Console.WriteLine(str + ": " + this.listenerRequest.Headers[str]);
            }
        }
        public void print(HttpRequestHeader req)
        {
            Console.WriteLine($"{req}: { this.listenerRequest.Headers[req.ToString()]}");
        }
    }
}