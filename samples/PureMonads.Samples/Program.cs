using PureMonads.Samples;

OptionSamples.Run();
await ResultSamples.RunAsync();
await PipeSamples.RunAsync();

Console.Write("To exit press any key...");
Console.ReadKey(true);