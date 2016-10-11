//---------------------------------------------------------------------
// File:	FactRetrieverForLoansProcessing.cs
// 
// Summary: 	Used to retrieve information from the CustInfo table 
//              added earlier to the Northwind sample SQL database.
//
// Sample: 	Loan Processing using Business Rules
//
//---------------------------------------------------------------------
// This file is part of the Microsoft BizTalk Server 2004 SDK
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// This source code is intended only as a supplement to Microsoft BizTalk
// Server 2004 release and/or on-line documentation. See these other
// materials for detailed information regarding Microsoft code samples.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
// KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
// PURPOSE.
//---------------------------------------------------------------------


using System;
using System.Collections;
using Microsoft.RuleEngine;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.Samples.BizTalk.LoansProcessingUsingBusinessRules.myFactRetriever
{
	
	/// Fact Retriever class for LoanProcessing Policy
	
	public class DbFactRetriever:IFactRetriever
	{
		
		public object UpdateFacts(RuleSetInfo rulesetInfo, Microsoft.RuleEngine.RuleEngine engine, object factsHandleIn)
		{
			object factsHandleOut;

			// The following logic asserts the required DB rows only once and always uses the the same values (cached) during the first retrieval in subsequent execution cycles
			if (factsHandleIn == null) 
			{
				string strCmd1 = "Persist Security Info=False;Integrated Security=SSPI;database=northwind;server=jinli2000";
				
				SqlConnection con1 = new SqlConnection(strCmd1);

				// Using data connection binding
				// DataConnection dc1 = new DataConnection("Northwind", "CustInfo", con1);
				
				// Using data table binding
				SqlDataAdapter dAdapt1 = new SqlDataAdapter();
				dAdapt1.TableMappings.Add("Table", "CustInfo");
				con1.Open();
				SqlCommand myCommand = new SqlCommand("SELECT * FROM CustInfo", con1);
				myCommand.CommandType = CommandType.Text;
				dAdapt1.SelectCommand = myCommand;
				DataSet ds = new DataSet("Northwind");
				dAdapt1.Fill(ds);
				TypedDataTable tdt1 = new TypedDataTable(ds.Tables["CustInfo"]);

  			    engine.Assert(tdt1);
				factsHandleOut = tdt1;

			}

			else
				factsHandleOut = factsHandleIn;

			return factsHandleOut;
		}


	}
}
