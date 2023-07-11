# Mail.API

The Mail.API is a simple .NET 6.0-based API used for sending emails using SMTP. It is built around a single `EmailController` that sends an email when a POST request is made.

## Installation

To use this project, clone the repository to your local machine and open it in your preferred .NET-compatible IDE (such as Visual Studio, Visual Studio Code, or JetBrains Rider).

## Configuration

The SMTP settings are stored in the `appsettings.json` file under the `SmtpSettings` key:

You should replace the placeholders with your actual SMTP settings. 

**Note: Make sure to not commit sensitive data (like passwords) to your version control system.**

## Usage

Once you've set up the SMTP settings, you can start the API and make a POST request to the `/api/Email` endpoint with the `name`, `email`, and `message` form data.

The `EmailController`'s `SendEmail` method will create and send an email using the SMTP settings from the configuration.

If the email is sent successfully, the API will respond with a 200 status code and the message "Message successfully sent!". If there's an error, the API will respond with a 500 status code and the error message.