namespace Playground;

public class Program
{
  public static async Task Main()
  {
    Basics.Run();
    CollectionsLINQ.Run();
    await RecordsClassesInterfaces.Run();
    AsyncStuff.Run();
    PatternMatchingSwitch.Run();
    Inheritance.Run();
    await Exceptions.Run();
    DependencyInjection.Run();
    ResultsDemo.Run();
    OrderReport.Run();
    await EnumDemo.Run();
    await AsyncPricing.Run();

    // Test zone
    ScratchPad.Run();
  }
}
