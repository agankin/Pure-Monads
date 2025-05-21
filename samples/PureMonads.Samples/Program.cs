using PureMonads.Samples;

OptionSamples.Run();
await AsyncOptionSamples.RunAsync();
await ResultSamples.RunAsync();
EitherSamples.Run();
await PipeSamples.RunAsync();

Console.Write("To exit press any key...");
Console.ReadKey(true);