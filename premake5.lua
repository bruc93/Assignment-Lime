-- Solution configuration file.
solution "Project"
configurations { "Debug" }
platforms { "Win64" }
    project "Project"
        targetname "Project"
        location "Project"
        kind "SharedLib"
        language "C#"
        files { "Project/**.cs" } 
        excludes { "**/bin/**", "**/obj/**" } 
        links { "System","System.Core","Microsoft.CSharp","System.Runtime.Serialization","System.ComponentModel.DataAnnotations" }

        configuration "Debug"
            defines { "TRACE","DEBUG" }
            optimize "On"
            targetdir "Project/bin/"
            kind "ConsoleApp"

        --postbuildcommands {"copy /Y $(ProjectDir)freebusy.txt $(ProjectDir)bin\kekland.txt"}
        postbuildcommands {"{COPYFILE} $(ProjectDir)freebusy.txt $(ProjectDir)bin\\kekland.txt"}
--Clean.
newaction {
    trigger = "clean",
    description = "Remove all binaries and intermediate binaries, and vs files.",
    execute = function()
        print("Removing binaries")
        os.rmdir("Project/bin")
        os.rmdir("Project/obj")

        print("Removing project files")
        os.rmdir("./.vs")
        os.remove("**.sln")
        os.remove("**.vcxproj")
        os.remove("**.vcxproj.filters")
        os.remove("**.vcxproj.user")
        print("Done")
    end
}