# Get-Started .NET/C#

Repository to help getting started with MongoDB .NET/C# driver connecting to MongoDB Atlas.

## Information

This Get-Started project uses [MongoDB .NET/C# driver](http://mongodb.github.io/mongo-csharp-driver/) version 2.12.1 by default. Although you can change the driver version, the provided code example was only tested against the default version of MongoDB driver. There is no guarantee that the code sample will work for all possible versions of the driver.

## Pre-requisites 

### Docker 

Have Docker running on your machine. You can download and install from: https://docs.docker.com/install/

### MongoDB Atlas

In order to execute the code example, you need to specify `MONGODB_URI` environment variable to connect to a MongoDB cluster. If you don't have any you can create one by signing up [MongoDB Atlas Free-tier M0](https://docs.atlas.mongodb.com/getting-started/). 

##  Execution Steps 

Execute the helper shell script followed by the MongoDB URI that you would like to connect to. 
```
./get-started.sh "mongodb+srv://usr:pwd@example.mongodb.net/dbname?retryWrites=true"
```

To use a different driver version, specify the driver version after the MongoDB URI. For example:
```
./get-started.sh "mongodb+srv://usr:pwd@example.mongodb.net/dbname?retryWrites=true" 2.11.3
```

## Tutorials

* [MongoDB .NET/C# driver documentation](https://docs.mongodb.com/drivers/csharp/)
* [Quickstart C# and MongoDB: Starting and Setup](https://www.mongodb.com/blog/post/quick-start-c-sharp-and-mongodb-starting-and-setup)

## Misc

* The use of .Net Core version 2.1+ (with SNI support) is required to connect to MongoDB Atlas shared instances (currently M0/M2/M5).

## About 

This project is part of the MongoDB Get-Started code examples. Please see [get-started-readme](https://github.com/mongodb-developer/get-started-readme) for more information. 

## Disclaimer

This software is not supported by [MongoDB, Inc](https://www.mongodb.com)
under any of their commercial support subscriptions or otherwise. Any usage is at your own risk.
