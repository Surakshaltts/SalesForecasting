# Sales Forecasting Application

This application provides sales forecasting based on historical sales data. It allows users to query total sales by year, query sales for a specific state by year, apply a percentage increment to sales, and export forecasted data to a CSV file. Additionally, users can apply individual percentage increases per state.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [File Structure](#file-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- Query total sales by year
- Apply a percentage increment to sales
- Export forecasted data to CSV

## Installation

1. *Clone the repository:*

    sh
    git clone https://github.com/yourusername/SalesForecastingApp.git
    cd SalesForecastingApp
    

2. *Set up the database:*

    Ensure you have a SQL Server instance running. Update the connection string in Program.cs with your SQL Server credentials.

3. *Build the project:*

    Open the project in Visual Studio or use the .NET CLI:

    sh
    dotnet build
    

## Usage

1. *Run the application:*

    sh
    dotnet run
    

2. *Select an option from the menu:*

    - Query total sales by year
    - Apply a percentage increment to sales
    - Export forecasted data to CSV
    - Exit

3. *Follow the prompts to input data as required:*

    For example, when querying total sales by year, enter the year when prompted.

4. *Export to CSV:*

    When exporting data to CSV, provide a valid file path where the CSV file should be saved. Example paths:
    
    - *Windows:* C:\Users\YourUsername\Documents\forecasted_sales.csv

## File Structure

- *Database*
  - SqlHelper.cs: Contains methods for executing SQL queries.
- *Models*
  - SalesData.cs: Represents the sales data model.
- *Services*
  - SalesService.cs: Contains business logic for sales forecasting.
- *Program.cs*: Main entry point of the application.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

1. Fork the repository.
2. Create a new branch (git checkout -b feature-branch).
3. Make your changes.
4. Commit your changes (git commit -am 'Add new feature').
5. Push to the branch (git push origin feature-branch).
6. Open a pull request.

