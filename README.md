# Get-Started .NET/C#

Repository to help getting started with MongoDB .NET/C# driver connecting to MongoDB Atlas.

## Information

This Get-Started project uses [MongoDB .NET/C# driver](http://mongodb.github.io/mongo-csharp-driver/) version 2.9.2 by default. Although you can change the driver version, the provided code example was only tested against the default version of MongoDB driver. There is no guarantee that the code sample will work for all possible versions of the driver.

## Pre-requisites 

### Docker 

Have Docker running on your machine. You can download and install from: https://docs.docker.com/install/

### MongoDB Atlas

In order to execute the code example, you need to specify `MONGODB_URI` environment variable to connect to a MongoDB cluster. If you don't have any you can create one by signing up [MongoDB Atlas Free-tier M0](https://docs.atlas.mongodb.com/getting-started/). 

## Build Steps 

1. Build Docker image with a tag name. Within this directory execute: 
   * To use the default driver version and specify `MONGODB_URI`:
      ```
      docker build . -t start-dotnet --build-arg MONGODB_URI="mongodb+srv://usr:pwd@example.mongodb.net/dbname?retryWrites=true"
      ```
   * To use a different driver version and specify `MONGODB_URI`. For example:
      ```
      docker build . -t start-dotnet --build-arg DRIVER_VERSION=2.8.2 --build-arg MONGODB_URI="mongodb+srv://usr:pwd@example.mongodb.net/dbname?retryWrites=true"
      ```
   This will build a docker image with a tag name `start-dotnet`. 
   As a result of the build, the example code is compiled for the specified driver version and ready to be executed.

2. Run the Docker image by executing:
   ```
   docker run --tty --interactive --hostname dotnet start-dotnet
   ```

   The command above will run a `start-dotnet` tagged Docker image. Sets the hostname as `dotnet`. 

## Execution Steps

1. Run the compiled C# code example by following below steps:
   * `cd ~/dotnet`
   * `dotnet build`
   * `dotnet run`

### Change driver version from within the Docker environment

For running the code example in a different driver version from the one built on the image:

1. Change the driver version within the project file [getstarted.csproj](dotnet/getstarted.csproj#L7)
2. Re-build the sample code:
   ```
   dotnet build
   ```

From within the docker environment, you can also change the `MONGODB_URI` by changing the environment variable: 

```sh
export MONGODB_URI="mongodb+srv://usr:pwd@new.mongodb.net/dbname?retryWrites=true"
```


## Tutorials

* [Quickstart C# and MongoDB: Starting and Setup](https://www.mongodb.com/blog/post/quick-start-c-sharp-and-mongodb-starting-and-setup)
* [MongoDB .NET/C# driver: Getting Started](https://mongodb.github.io/mongo-csharp-driver/2.9/getting_started/quick_tour/)

## Misc

* The use of .Net Core version 2.1+ (with SNI support) is required to connect to MongoDB Atlas shared instances (currently M0/M2/M5).

## About 

This project is part of the MongoDB Get-Started code examples. Please see [get-started-readme](https://github.com/mongodb-developer/get-started-readme) for more information. 
