// Allows for the use of unique data structures
using System.Collections.Generic;
// Used to allow for async tasks
using System.Threading.Tasks;

// Importing CLIWrap libraries to interact with command-line interfaces
using CliWrap;
// allows for the capture of outputs and errors in memory while running commands
// i.e. useful to read what the executions write to the console
using CliWrap.Buffered;

public class ProgramLister
{
    // define a default set of programs based on the given example
    // privated to only allow the containing class to access it and determine
    // how much the list itself can be touched
    private List<PROGRAM> programList = new List<PROGRAM>
    {
        new PROGRAM("node"),
        new PROGRAM("npm"),
        new PROGRAM("python"),
        new PROGRAM("yarn")
    };

    public List<PROGRAM> GetProgramList()
    {
        return programList;
    }

    // Adders and removers
    public void AddProgram(PROGRAM program)
    {
        programList.Add(program);
    }

    public void RemoveProgram(PROGRAM program)
    {
        programList.Remove(program);
    }

    public void ClearProgramList()
    {
        programList.Clear();
    }

    // Provided the user messed up somehow and would like to restore to the default
    // set of programs
    public void RestoreProgramListDefault()
    {
        programList = new List<PROGRAM>
        {
            new PROGRAM("node"),
            new PROGRAM("npm"),
            new PROGRAM("python"),
            new PROGRAM("yarn")
        };
    }

    // Empty default constructor to allow for the creation of the lister with just
    // the bare bones
    public ProgramLister()
    {

    }

    // Helper function to allow version fetching for the function below
    private async Task<string> GetProgramVersion(PROGRAM p)
    {
        // try-catch block to handle cases where the programs aren't installed on the host machine
        try
        {
            // The Console.WriteLine calls within this try scope is for debugging purposes only
            Console.WriteLine("\nProgram Name: " + p.programName + " Argument: " + p.versionCommand + "\n");

            // utilize CLIWrap to execute commands
            var result = await Cli.Wrap(p.programName)
                                  .WithArguments(p.versionCommand)
                                  .ExecuteBufferedAsync();

            Console.WriteLine("Output: " + result.StandardError.Trim());
            Console.WriteLine("Output: " + result.StandardOutput.Trim());

            Console.WriteLine(result.StandardOutput.Trim().Length);

            // provided things went well, assign the result to the PROGRAM's version
            p.programVersion = result.StandardOutput.Trim();

            // return the result as a string as well (provided that it's necessary for another user)
            return result.StandardOutput.Trim();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return "Not installed or not found in PATH";
        }
    }

    // Fetch versions for all programs
    public async Task FetchAllVersions()
    {
        foreach (var program in programList)
        {
            await this.GetProgramVersion(program);
        }
    }
}