using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Threading.Tasks;

namespace helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            RecognizeSpeechAsync().Wait();
            Console.WriteLine("Please press <Return> to continue.");
            Console.ReadLine();
        }

        public static async Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription("f36e26ed19334a5381b09bd28425ff49", "northeurope");
            using (var recognizer = new SpeechRecognizer(config, AudioConfig.FromDefaultMicrophoneInput()))
            {
                Console.WriteLine("Say something...");
                var result = await recognizer.RecognizeOnceAsync();
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"We recognized: {result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
        }
    }
}
