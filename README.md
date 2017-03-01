# Airline Course Database
#### _Site to view courses_

#### By _**Renee Mei && Minh Phuong**_

## Description

This website will take a string and a word from a user, then count the number of times that word occurs inside of the string.

***

## Setup/Installation Requirements

#### Create Databases
* In `SQLCMD`:  
        `> CREATE DATABASE registrar`  
        `> GO`  
        `> USE registrar`  
        `> GO`  
        `> CREATE TABLE students(id INT IDENTITY(1,1),name VARCHAR(255), enroll_date VARCHAR(255))`
        `> GO`  
        `> CREATE TABLE courses(id INT IDENTITY(1,1),course_name VARCHAR(255), course_number VARCHAR(255))`
        `> GO`  
        `> CREATE TABLE students_courses (id INT IDENTITY(1,1), student_id INT, course_id INT)`  
        `> GO`  

* Requires DNU, DNX, MSSQL, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

***

## Specifications

### Student Class
================  

**The DeleteAll method for the Student class will delete all rows from the students table.**
* Example Input: none
* Example Output: nothing

**The GetAll method for the Student class will return an empty list if there are no entries in the Student table.**
* Example Input: N/A
* Example Output: empty list

**The Equals method for the Student class will return true if the Student in local memory matches the Student pulled from the database.**
* Example Input:  
        > Local: "Joe", id is 10 , dateEnrolled: Fall 2016
        > Database: "Joe", id is 10 , dateEnrolled: Fall 2016
* Example Output: `true`

**The Save method for the Student class will save new students to the database.**
* Example Input:  
\> New studenty: "Joe"
* Example Output: no return value

**The Save method for the Student class will assign an id to each new instance of the Student class.**
* Example Input:  
\> New student: "Joe", `local id: 0`  
* Example Output:  
\> "Joe", `database-assigned id`  

**The GetAll method for the Student class will return all student entries in the database in the form of a list.**
* Example Input:  
        > "Joe", id is 10  
        > "Roy", id is 11  
* Example Output: `{Joe object}, {Roy object}`

**The Find method for the Student class will return the Student as defined in the database.**
* Example Input: "Joe"
* Example Output: "Joe", `database-assigned id`

**The Update method for the Student class will return the Student with the new student name or date of enrollment.**
* Example Input: "Joe", id is 10 , dateEnrolled: Fall 2016
* Example Output: "Joe", id is 10 , dateEnrolled: Fall 2017

**The DeleteThis method for the Student class will return a list of Students without the deleted Course.**
* Example Input: "Joe"
* Example Output: "Roy"

**The Search method for the Student class will return a list of Students with matched name or date of enrollment.**
* Example Input: Search "Joe
* Example Output: "Joe", id is 10 , dateEnrolled: Fall 2016

### Course class
================

**The DeleteAll method for the Course class will delete all rows from the courses table.**
* Example Input: none
* Example Output: nothing

**The GetAll method for the Course class will return an empty list if there are no entries in the Course table.**
* Example Input: N/A, automatically loads on home page
* Example Output: empty list

**The Equals method for the Course class will return true if the Course in local memory matches the Course pulled from the database.**
* Example Input:  
        > Local: "HIST100", id is 10  
        > Database: "HIST100", id is 10  
* Example Output: `true`

**The Save method for the Course class will save new Courses to the database.**
* Example Input:  
\> New course: "HIST100"
* Example Output: no return value

**The Save method for the Course class will assign an id to each new instance of the Course class.**
* Example Input:  
\> New course: "HIST100", `local id: 0`  
* Example Output:  
\> "HIST100",`database-assigned id`  

**The GetAll method for the Course class will return all course entries in the database in the form of a list.**
* Example Input:  
        > "HIST100", id is 10  
        > "HIST101", id is 11  
* Example Output: `{HIST100 object}, {HIST101 object}`

**The Find method for the Course class will return the Course as defined in the database.**
* Example Input: "HIST100",
* Example Output: "HIST100", `database-assigned id`

**The Update method for the Course class will return the Course with the new course name.**
* Example Input: "HIST100", id is 10, courseName: United States History from 1877
* Example Output: "HIST100", id is 10, courseName: United States History to 1877

**The DeleteThis method for the Course class will return a list of Courses without the deleted Course.**
* Example Input: "HIST100"
* Example Output: "HIST101"

**The Search method for the Course class will return a list of Courses with matched course name or course number.**
* Example Input: Search "HIST10"
* Example Output: "HIST100", id is 10, courseName: United States History to 1877, "HIST101", id is 11, courseName: Britain History to 1877

### Student && Course classes
===========================

**The GetCourse method for the Student class will return the list of courses associated with that student.**
* Example Input: "Joe"
* Example Output:  "HIST100", "HIST101"

**The GetStudent method for the Course class will return the list of students associated with that course.**
* Example Input: "HIST100"
* Example Output: "Joe", "Roy"

**The Delete method for the Student class will delete the entry that connects the course id with the student**
* Example Input: Delete "HIST101" from "Joe"
* Example Output: List of all courses excluding the combination of "HIST101" and "Joe"

### Department class
===========================

**The DeleteAll method for the Department class will delete all rows from the courses table.**
* Example Input: none
* Example Output: nothing

**The GetAll method for the Department class will return an empty list if there are no entries in the Department table.**
* Example Input: N/A, automatically loads on home page
* Example Output: empty list

**The Equals method for the Department class will return true if the Department in local memory matches the Department pulled from the database.**
* Example Input:  
        > Local: "Sociology", id is 10  
        > Database: "Sociology", id is 10  
* Example Output: `true`

**The Save method for the Department class will save new Departments to the database.**
* Example Input:  
\> New course: "HIST100"
* Example Output: no return value

**The Save method for the Department class will assign an id to each new instance of the Course class.**
* Example Input:  
\> New course: "HIST100", `local id: 0`  
* Example Output:  
\> "HIST100",`database-assigned id`  


**The GetDept method for the Department class will return the list of departments.**
* Example Input:  
        > "Sociology", id is 10  
        > "History", id is 11  
* Example Output: `{Sociology object}, {History object}`

**The GetDeptStudent method for the Student class will return the list of department associated with that student.**
* Example Input: "Joe"
* Example Output: "Sociology"

**The GetDeptCourse method for the Course class will return the list of department associated with that course.**
* Example Input: "HIST100"
* Example Output: "History"

**The GetStudent method for the Course class will return the list of students associated with that course.**
* Example Input: "HIST100"
* Example Output: "Joe", "Roy"

**The Delete method for the Student class will delete the entry that connects the course id with the student**
* Example Input: Delete "HIST101" from "Joe"
* Example Output: List of all courses excluding the combination of "HIST101" and "Joe"

### User Interface
===================  

**The user can add a new Student using the "Add Student" form.**
* Example Input: New Student: "Joe", enrollment date: Fall 2017;
* Example Output: All Students Page: "Joe,Roy,Kim"

**The user can add a new Course using the "Add course" form.**
* Example Input: New Course: "HIST100", courseName: United States History from 1877
* Example Output: All Courses:"HIST100", "HIST101", "HIST102"

**The user can click on any course in the courses list to view the course's details**
* Example Input: *click* "HIST100"
* Example Output: "HIST100", courseName: United States History from 1877, students enrolled "Joe"

**The user can click on any student to view a list of all courses that the student is enrolled in and enrollment date.**
* Example Input: *click* "Joe"
* Example Output: "Joe", enrollment date: Fall 2017; enrolled courses: "HIST100", courseName: United States History from 1877; "HIST101", courseName: Britain History to 1877

**The user can edit a course's course name and course number on the course's page.**
* Example Input:  
    \> *click* "HIST100"  
    \> New course name: "Britan History to 1877"  
* Example Output: "HIST100", "Britan History to 1877"

**The user can delete a course using a link on the course's page which will lead to a confirmation page.**
* Example Input:  
    \> *click* "HIST100"  
    \> *delete clicky*  
    \> *confirmation clicky*
* Example Output: Return to Search Course Page

**The user can edit a student's name and enrollment date on the student's page.**
* Example Input:  
    \> *click* "Joe"  
    \> New enrollment date: "Fall 2017"  
* Example Output: "HIST100", "Fall 2017"

**The user can delete a student using a link on the student's page which will lead to a confirmation page.**
* Example Input:  
    \> *click* "Joe"  
    \> *delete clicky*  
    \> *confirmation clicky*
* Example Output: Return to Search Student Page

**The user can search using student name and enrollment date for a student using the search form.**
* Example Input: "Jo", "Fall 2016"
* Example Output: "Joe"

**The user can search using course name and course number for a course using the search form.**
* Example Input: "History" "HIST10"
* Example Output: "HIST101"


**The user can add a department to a student using selection form.**
* Example Input: "Joe", "History"
* Example Output: "Joe, "History", "Sociology"

**The user can add a department to a course using a selection form.**
* Example Input: "Intro to US History", "History"
* Example Output: "Intro to US History", "History"


***

## Support and contact details

Please contact Renee Mei at meiqianye@gmail.com or Minh Phuong mphuong@kent.edu with any questions, concerns, or suggestions.

***

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor
* MSSQL & SSMS

***

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 **_Renee Mei && Minh Phuong_**
