using PureMonads.Samples;

await OptionSamples.RunAsync();
await AsyncOptionSamples.RunAsync();
await ResultSamples.RunAsync();
EitherSamples.Run();
await PipeSamples.RunAsync();
await PipeMapSamples.RunAsync();

Console.Write("To exit press any key...");
Console.ReadKey(true);