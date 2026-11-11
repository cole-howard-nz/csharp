namespace Playground;

public class Program
{
  public static async Task Main()
  {
    Basics.Run();
    CollectionsLINQ.Run();
    RecordsClassesInterfaces.Run();
    AsyncStuff.Run();
    PatternMatchingSwitch.Run();
    Inheritance.Run();
    Exceptions.Run();
    DependencyInjection.Run();
    ResultsDemo.Run();
    OrderReport.Run();
    EnumDemo.Run();

    // Test zone
    ScratchPad.Run();
  }
}
