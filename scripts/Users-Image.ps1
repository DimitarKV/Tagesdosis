param (
   [string]$imageName = "tagesdosis-user",
   [Parameter(Mandatory=$true)][string]$action,
   [Parameter(Mandatory=$true)][string]$technology
)

function Create-Image {
	[CmdletBinding()]
	param(
		[Parameter()]
		[string] $technology
	)
	
    if ($technology -eq "rest")
    {
        docker build -t $imageName -f ../src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Api/Dockerfile ../
        return;
    }
    if ($technology -eq "grpc")
    {
        docker build -t $imageName -f ../src/Services/Tagesdosis.Services.User/Tagesdosis.Services.User.Grpc/Dockerfile ../
        return;
    }

    Write-Output "The user microservice does not support this technology"
}

switch ($action)
{
    "build" { Create-Image -technology $technology}
    "push" {}
    Default { write-output "No such action." }
}