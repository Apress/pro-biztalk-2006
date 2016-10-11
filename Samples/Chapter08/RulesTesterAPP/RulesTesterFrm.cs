using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;
using Microsoft.RuleEngine;


namespace RulesTesterApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class RulesTesterFrm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblProcessing;
		private System.Windows.Forms.TextBox inboundTxtBx;
		private System.Windows.Forms.TextBox outboundTxtBx;
		private System.Windows.Forms.TextBox ProcessingTxtBx;
		private System.Windows.Forms.TextBox traceTxtBx;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button processBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RulesTesterFrm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.processBtn = new System.Windows.Forms.Button();
			this.traceTxtBx = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.ProcessingTxtBx = new System.Windows.Forms.TextBox();
			this.outboundTxtBx = new System.Windows.Forms.TextBox();
			this.inboundTxtBx = new System.Windows.Forms.TextBox();
			this.lblProcessing = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.processBtn);
			this.groupBox1.Controls.Add(this.traceTxtBx);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.ProcessingTxtBx);
			this.groupBox1.Controls.Add(this.outboundTxtBx);
			this.groupBox1.Controls.Add(this.inboundTxtBx);
			this.groupBox1.Controls.Add(this.lblProcessing);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(536, 296);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Start Passing Docs";
			// 
			// processBtn
			// 
			this.processBtn.Location = new System.Drawing.Point(352, 96);
			this.processBtn.Name = "processBtn";
			this.processBtn.Size = new System.Drawing.Size(152, 23);
			this.processBtn.TabIndex = 8;
			this.processBtn.Text = "Start Processing";
			this.processBtn.Click += new System.EventHandler(this.processBtn_Click);
			// 
			// traceTxtBx
			// 
			this.traceTxtBx.Location = new System.Drawing.Point(200, 72);
			this.traceTxtBx.Name = "traceTxtBx";
			this.traceTxtBx.Size = new System.Drawing.Size(320, 20);
			this.traceTxtBx.TabIndex = 7;
			this.traceTxtBx.Text = "F:\\\\RulesTest\\\\TraceFiles\\\\";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(16, 75);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(168, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Trace Files Folder";
			// 
			// ProcessingTxtBx
			// 
			this.ProcessingTxtBx.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProcessingTxtBx.Location = new System.Drawing.Point(3, 157);
			this.ProcessingTxtBx.Multiline = true;
			this.ProcessingTxtBx.Name = "ProcessingTxtBx";
			this.ProcessingTxtBx.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ProcessingTxtBx.Size = new System.Drawing.Size(530, 136);
			this.ProcessingTxtBx.TabIndex = 5;
			this.ProcessingTxtBx.Text = "";
			// 
			// outboundTxtBx
			// 
			this.outboundTxtBx.Location = new System.Drawing.Point(200, 48);
			this.outboundTxtBx.Name = "outboundTxtBx";
			this.outboundTxtBx.Size = new System.Drawing.Size(320, 20);
			this.outboundTxtBx.TabIndex = 4;
			this.outboundTxtBx.Text = "F:\\\\RulesTest\\\\Processed\\\\";
			// 
			// inboundTxtBx
			// 
			this.inboundTxtBx.Location = new System.Drawing.Point(200, 20);
			this.inboundTxtBx.Name = "inboundTxtBx";
			this.inboundTxtBx.Size = new System.Drawing.Size(320, 20);
			this.inboundTxtBx.TabIndex = 3;
			this.inboundTxtBx.Text = "F:\\\\RulesTest\\\\";
			// 
			// lblProcessing
			// 
			this.lblProcessing.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProcessing.Location = new System.Drawing.Point(8, 130);
			this.lblProcessing.Name = "lblProcessing";
			this.lblProcessing.Size = new System.Drawing.Size(496, 16);
			this.lblProcessing.TabIndex = 2;
			this.lblProcessing.Text = "Presently Processing";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Outbound From Rules Folder";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Inbound To Rules Folder";
			// 
			// RulesTesterFrm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 308);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "RulesTesterFrm";
			this.Text = "Rules Tester App";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new RulesTesterFrm());
		}

		private void processBtn_Click(object sender, System.EventArgs e)
		{
			string destDirectory = outboundTxtBx.Text.Trim();
			string root = inboundTxtBx.Text.Trim();
			string [] files = Directory.GetFiles(root);
			if(files.Length == 0 )
				MessageBox.Show("No Files to Process","Did You Forget Something?",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			for ( int i = 0 ; i < files.Length ; i++ )
			{
				string fileName = files[i].Trim();
					
				FileInfo fileInfo = new FileInfo(fileName);
				if (fileInfo.Extension.ToUpper() == ".XML" )
				{
					string dFileName = fileInfo.Name;
					dFileName = dFileName.Substring(0,dFileName.Length-4);
					
					StreamReader sr = new StreamReader(fileName);
					string s = sr.ReadToEnd();
					
					XmlDocument xd = new XmlDocument();
					xd.LoadXml(s);
					ProcessingTxtBx.Text = ProcessingTxtBx.Text + "Processing The Document: " + fileName + "\r\n";
					FireRule(xd, dFileName);
					ProcessingTxtBx.Text = ProcessingTxtBx.Text + "Processing Completed for the document: " + fileName + "\r\n";
					sr.Close();
					string destFileName = dFileName;
					destFileName = destDirectory + destFileName + fileInfo.Extension;
					if (File.Exists(destFileName))
						File.Delete(destFileName);
					File.Move(fileName,destFileName);
				}
			}
		}
		
		private void FireRule(XmlDocument xd, string fileName)
		{

			string destDirectory = outboundTxtBx.Text.Trim();
			string traceDirectory = traceTxtBx.Text.Trim();
			TypedXmlDocument doc1 = new TypedXmlDocument("TrackSource.BizTalk.Schemas.InboundToRules", xd);

			string [] policies = new string[14] {
													"Data Preparation",
													"Pre-Processing Group 1",
													"Pre-Processing Group 2",
													"Pre-Processing Group 3",
													"Pre-Processing Group 4",
													"Pre-Processing Group 5",
													"Pre-Processing Group 6",
													"Lender Specific",
													"Insurance Servicing",
													"Post Processing Group 1",
													"Post Processing Group 2",
													"Black Hole",
													"Fidelity Hold",
													"Procedure Set"
												};
				
			ProcessingTxtBx.Text = ProcessingTxtBx.Text +  "Processing Started " +  DateTime.Now + "\r\n";
			string traceFileName = fileName + "_RuleTrace";
			traceFileName = traceDirectory + traceFileName + ".txt";
			if (File.Exists(traceFileName))
				File.Delete(traceFileName);
			DebugTrackingInterceptor dti = new DebugTrackingInterceptor(traceFileName);
			
			try
			{
				for( int i = 0 ; i < policies.Length; i++ )
				{
					string PolicyName = policies[i].Trim();
					lblProcessing.Text = PolicyName;
					ProcessingTxtBx.Text = ProcessingTxtBx.Text +  "Processing ... " + policies[i] + " " +  DateTime.Now + "\r\n";
					Application.DoEvents();
					Microsoft.RuleEngine.Policy tstPolicy = new Microsoft.RuleEngine.Policy(PolicyName);
					ArrayList shortTermFacts = new ArrayList();
					shortTermFacts =GetFacts(PolicyName);
					shortTermFacts.Add(doc1);
					tstPolicy.Execute(shortTermFacts.ToArray(), dti );
					tstPolicy = null;
					
				}
			}
			catch(Exception ex)
			{
				ProcessingTxtBx.Text = ProcessingTxtBx.Text +  "Exception Caught Check _Excepion Text File";
				string exceptionFileName = fileName + "_Exception";
				exceptionFileName = traceDirectory + exceptionFileName + ".txt";
				if (File.Exists(exceptionFileName))
					File.Delete(exceptionFileName);
				StreamWriter sw2 = new StreamWriter(exceptionFileName);
				ProcessingTxtBx.Text = ProcessingTxtBx.Text + ex.Message;
				sw2.Write(ex.Message.ToString());
				sw2.Close();
			}
			finally
			{
				ProcessingTxtBx.Text = ProcessingTxtBx.Text +  "Processing Done " +  DateTime.Now + "\r\n";
				lblProcessing.Text =  "Writing output File" ;
				string processedDoc = fileName + "_Outbound";
				processedDoc = destDirectory + processedDoc + ".xml";
				if (File.Exists(processedDoc))
					File.Delete(processedDoc);
				StreamWriter sw = new StreamWriter(processedDoc);
				dti.CloseTraceFile();
				sw.Write(doc1.Document.InnerXml.ToString());
				sw.Close();
				xd = null;
				doc1 = null;
				lblProcessing.Text =  "Processing Completed for " + fileName ;

			}
			//return xd;
		}
		
		public ArrayList GetFacts(string policyName)
		{
			#region From rules Engine
			
			ArrayList shortermFacts = new ArrayList();
			Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver dd = new Microsoft.BizTalk.RuleEngineExtensions.RuleSetDeploymentDriver();
			RuleStore sqlrs;
			sqlrs = dd.GetRuleStore();
			RuleSetInfoCollection rsInfoCollection = sqlrs.GetRuleSets(policyName,RuleStore.Filter.Latest);
			foreach(RuleSetInfo rsInfo in rsInfoCollection)
			{
				RuleSet ruleset = sqlrs.GetRuleSet(rsInfo);
				ArrayList bindingList =Microsoft.RuleEngine.RuleSetBindingFinder.GetBindings(ruleset);
				foreach(object o in bindingList)
				{
					if(o is Microsoft.RuleEngine.ClassBinding)
					{
						Microsoft.RuleEngine.ClassBinding cb = o as Microsoft.RuleEngine.ClassBinding;
						if(cb.TypeName != "TrackSource.BizTalkRules.LongTermFactsWrapper")
							shortermFacts.Add(Activator.CreateInstance(cb.Type));
						
					}
				}

			}
			return shortermFacts;
			
			#endregion
		}
	}
}
