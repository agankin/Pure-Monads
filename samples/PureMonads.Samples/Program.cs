using PureMonads.Samples;

OptionSamples.Run();
await ResultSamples.RunAsync();
EitherSamples.Run();
await PipeSamples.RunAsync();

Console.Write("To exit press any key...");
Console.ReadKey(true);