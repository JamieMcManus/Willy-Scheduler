![alt text](https://i.ya-webdesign.com/images/willie-simpsons-png-13.png)

# Willy-Scheduler

The Willy Sceduler is a dot net command line poject that schedules the creation of list of CSV files from MYSQL stored procedures.  


Set the Stored Procedure , CSV Name , Path and TimeToRun in the ScheduleConfiguration.xml
Make sure to set the Connection String in the App.Config file !


Procedure : This is the name of the stored procedure

Name: This is the name of the file output + todays date appended.


- default : The CSV will be generated in root/CSV_Hub
eg
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="default" TimeToRun="0 0/1 * 1/1 * ? *" />

- NewFolder {yourfoldername}  : The CSV will be generated in root/CSV_Hub/{yourfoldername}
eg 
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="{yourfoldername}" TimeToRun="0 0/1 * 1/1 * ? *" />

- Full Path : The CSV will be generated at a path given by you 
eg
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="C://TestFolder" TimeToRun="0 0/1 * 1/1 * ? *" />


- TimeToRun: This is a cron expression to set how frequently the CSV will be generated.
see https://www.freeformatter.com/cron-expression-generator-quartz.html for more



