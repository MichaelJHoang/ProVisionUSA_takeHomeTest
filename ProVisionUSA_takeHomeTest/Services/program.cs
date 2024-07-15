// Class to contain the program name and version command which can be reused, modified, or perhaps extended
public class PROGRAM
{
    public string programName { get; set; }

    // Initially assume that each program contains these values unless specified otherwise
    public string programVersion { get; set; } = "Not installed or not found in PATH";
    public string versionCommand { get; set; } = "--version";

    // We don't want to have an empty program name to check the version of,
    // so I made the default constructor private
    private PROGRAM()
    {

    }

    // Instead, we want the following constructor to force a program name specification
    public PROGRAM(string programName)
    {
        this.programName = programName.ToLower();
    }

    // Utilize constructor chaining to also call the constructor above and reduce redundancies
    public PROGRAM(string programName, string versionCommand) : this(programName)
    {
        this.versionCommand = versionCommand;
    }
}