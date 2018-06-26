# Album Photos List

A simple listing of photos found in a user-specified album. (data borrowed from [JSONPlaceholder](https://jsonplaceholder.typicode.com/) )

## Getting Started

Clone project folders & files to the location of your choice.

### Prerequisites

*[Microsoft Core SDK 2.1.0](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300)


### Installing

The app _should_ run on any operating system with dotnet core installed.

## Running the tests

The AlbumPhotoList.Tests folder contains the test project.  
``` 
1.  navigate to test folder from command line.
2.  type dotnet test
3.  monitor test process
```

### Test breakdown

A total of 13 simple tests have been written.

```
1 test assures the default message is displayed if no additional parameters are provided.
5 tests verify that numeric input falls within acceptable range.
5 tests verify that only acceptable alpha input is evaluated for execution.
2 tests verify that http request calls do not result in fatal errors.
```

## Running the app

The AlbumPhotoList folder contains the executable project.  
``` 
1.  navigate to project folder from command line.
2.  type dotnet run
3.  follow on-screen prompts
```


