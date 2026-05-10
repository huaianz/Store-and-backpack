//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Student
//{
//    private string rollNo;
//    private string name;
//    public string getRollNo()
//    {
//        return rollNo;
//    }
//    public void setRollNo(string rollNo)
//    {
//        this.rollNo = rollNo;
//    }
//    public string getName()
//    {
//        return name;
//    }
//    public void setName(string name)
//    {
//        this.name = name;
//    }
//}

//public class StudentView
//{
//    public void PrintStudentDetails(string studentName, string studentRollNo)
//    {
//        Console.WriteLine("Student: ");
//        Console.WriteLine("Name: " + studentName);
//        Console.WriteLine("Roll No: " + studentRollNo);
//    }
//}

//public class StudentController
//{
//    private Student model;
//    private StudentView view;

//    public StudentController(Student model, StudentView view)
//    {
//        this.model = model;
//        this.view = view;
//    }

//    public void setStudentName(String name)
//    {
//        model.setName(name);
//    }

//    public String getStudentName()
//    {
//        return model.getName();
//    }

//    public void setStudentRollNo(String rollNo)
//    {
//        model.setRollNo(rollNo);
//    }

//    public String getStudentRollNo()
//    {
//        return model.getRollNo();
//    }

//    public void updateView()
//    {
//        view.PrintStudentDetails(model.getName(), model.getRollNo());
//    }
//}

//public class MVCPatternDemo
//{
//    public static void main(String[] args)
//    {
//        Student model = retrieveStudentFromDatabase();

//        StudentView view = new StudentView();

//        StudentController controller = new StudentController(model, view);

//        controller.updateView();

//        controller.setStudentName("John");

//        controller.updateView();
//    }

//    private static Student retrieveStudentFromDatabase()
//    {
//        Student student = new Student();
//        student.setName("Robert");
//        student.setRollNo("10");
//        return student;
//    }
//}
