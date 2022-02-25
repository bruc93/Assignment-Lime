-- Solution configuration file.
workspace "Project"
   configurations { "Debug", "Release" }

project "Application"
    kind "ConsoleApp"
    language "C#"
    files{"src/*.cs"}

    configuration "Debug"
        targetdir("src/Debug/bin/")
        objdir("src/Debug/obj")
        postbuildcommands {"{COPYFILE} $(ProjectDir)\\src\\freebusy.txt $(ProjectDir)\\src\\Debug\\bin\\freebusy.txt"}
    configuration "Release"
        targetdir("src/Release/bin/")
        objdir("src/Release/obj")
        postbuildcommands {"{COPYFILE} $(ProjectDir)\\src\\freebusy.txt $(ProjectDir)\\src\\Release\\bin\\freebusy.txt"}

--Clean.
newaction {
    trigger = "clean",
    description = "Remove all binaries and intermediate binaries, and vs files.",
    execute = function()
        print("Removing binaries")
        os.rmdir("src/Debug")
        os.rmdir("src/Release")

        print("Removing project files")
        os.rmdir("./.vs")
        os.remove("**.sln")
        os.remove("**.vcxproj")
        os.remove("**.vcxproj.filters")
        os.remove("**.vcxproj.user")
        os.remove("**.csproj.user")
        os.remove("**.csproj")
        print("Done")
    end
}