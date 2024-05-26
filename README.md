# DocumentTools: An Azure Function Solution

This repository provides a set of Azure Functions for working with documents. 

## Prerequisites

- **Azure Functions Core Tools:** You'll need the Azure Functions Core Tools installed to develop and run your functions locally. Download and install them from the official Microsoft documentation: [https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)

## Getting Started

1. **Clone the Repository:** Clone this repository to your local machine using Git.
2. **Run the Functions Locally:** Open a terminal in the project directory and run the following command to start the local Azure Functions runtime:

```bash
func start
```

This will start the local development server and expose your functions on `http://localhost:7071`.

## APIs

The Azure Functions project exposes two main APIs:

1. **GetDocumentFromZip (GET/POST):** 
    * This function retrieves a specific document from a base64 encoded zip archive.
    * **Request Body (JSON):**
        ```json
        {
          "Zip": "base64_encoded_zip_content",
          "Index": 0
        }
        ```
    * **Response:**
        * On success, the function returns the requested document as a file download with the content type set to `application/octet-stream`.
        * On error, a JSON object containing the error message is returned with a status code of 400 (Bad Request).

2. **Entries (GET/POST):** 
    * This function retrieves a list of entries (filenames) within a base64 encoded zip archive.
    * **Request Body (JSON):**
        ```json
        {
          "Zip": "base64_encoded_zip_content"
        }
        ```
    * **Response:**
        * On success, the function returns a JSON array containing the names of all entries within the zip archive.
        * On error, a JSON object containing the error message is returned with a status code of 400 (Bad Request).

## Example Usage with Postman

You can use tools like Postman to test these APIs locally by sending HTTP requests to `http://localhost:7071`. Here's how to test each function:

### GetDocumentFromZip

1. Set the request method to `POST`.
2. Set the request body to RAW mode and paste the following JSON replacing the placeholder with your base64 encoded zip and desired document index:

```json
{
  "Zip": "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA==",
  "Index": 0 
}
```

3. Send the request.

### Entries

1. Set the request method to `POST`.
2. Set the request body to RAW mode and paste the following JSON replacing the placeholder with your base64 encoded zip:

```json
{
  "Zip": "UEsDBBQAAAAAAIdbulgAAAAAAAAAAAAAAAAIAAAAVGVzdFppcC9QSwMECgAAAAAAilu6WMvQ3WkLAAAACwAAABEAAABUZXN0WmlwL1Rlc3QxLnR4dEkgYW0gdGVzdCAxUEsBAj8AFAAAAAAAh1u6WAAAAAAAAAAAAAAAAAgAJAAAAAAAAAAQAAAAAAAAAFRlc3RaaXAvCgAgAAAAAAABABgAPblVaVev2gE9uVVpV6/aAXB8tV5Xr9oBUEsBAj8ACgAAAAAAilu6WMvQ3WkLAAAACwAAABEAJAAAAAAAAAAgAAAAJgAAAFRlc3RaaXAvVGVzdDEudHh0CgAgAAAAAAABABgAlHI/bVev2gGQN0NtV6/aAanwoWZXr9oBUEsFBgAAAAACAAIAvQAAAGAAAAAAAA=="
}