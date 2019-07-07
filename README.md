![alt text](https://i.ya-webdesign.com/images/willie-simpsons-png-13.png)

# Willy-Scheduler

---

## The Willy Sceduler is a dot net command line project that schedules the creation of list of CSV files from MYSQL stored procedures.  

Make sure to set the Connection String in the App.Config file !

Set the Stored Procedure , CSV Name , Path and TimeToRun in the ScheduleConfiguration.xml

The Options are as follows :

1.Procedure : This is the name of the stored procedure

---

2.Name: This is the name of the file output + todays date appended.

---

3.Path has the Following options :
* default : The CSV will be generated in root/CSV_Hub
eg
```xml
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="default" TimeToRun="0 0/1 * 1/1 * ? *" />
```

* NewFolder {yourfoldername}  : The CSV will be generated in root/CSV_Hub/{yourfoldername}
eg 

```xml
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="{yourfoldername}" TimeToRun="0 0/1 * 1/1 * ? *" />`
```

* Full Path : The CSV will be generated at a path given by you 
eg

```xml
<ReportItem Procedure="GetTopSales" Name="TopSellersReport" Path="C://TestFolder" TimeToRun="0 0/1 * 1/1 * ? *" />
```

---

4.TimeToRun: This is a cron expression to set how frequently the CSV will be generated.
see https://www.freeformatter.com/cron-expression-generator-quartz.html for more

---

##### Planned Addition

 ☐ SQL Server Connection Option 


---

and remember 
> ### Willie Hears Ya, Willie Don't Care


