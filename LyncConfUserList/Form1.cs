using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Lync.Model;
using Microsoft.Lync.Model.Conversation;
using Microsoft.Lync.Model.Extensibility;

namespace LyncConfUserList
{

    //{33F41092-47E5-43BD-97AC-1EFA7A6B9F33}
    public partial class Form1 : Form
    {
       // EndPointInformation endPointInformation;   //定义EndPoint对象

        public LyncClient _Client;

        public Microsoft.Lync.Model.Conversation.ConversationManager _ConversationMgr;
        //private InstantMessageModality _LocalIMModality;
        public Microsoft.Lync.Model.Conversation.Conversation _LycConversation;
        public Boolean _InitializeFlag;
        public ConversationWindow _conversationWindow;
        public string CurrentUserName;
        private BindingSource bindingSource1 = new BindingSource();
        bool isExported;
        bool needUpdated;
        DataTable dtConv;
        DataTable dtPar;
        public Form1()
        {
            InitializeComponent();
            InitializeConList();
        }

        private void InitializeConList()
        {
            try
            {


                _Client = LyncClient.GetClient();
                SignInConfiguration sc = _Client.SignInConfiguration;
                if (sc != null)
                {
                    CurrentUserName = sc.UserName.ToString();
                }
                CurrentUserName = _Client.Uri.ToString().Replace("sip:", "");
                _ConversationMgr = _Client.ConversationManager;
                _ConversationMgr.ConversationAdded += ConversationAdd;
                _ConversationMgr.ConversationRemoved += ConversationRemoved;
                conbox1.Items.Add("Pls Select A Item");
                dtConv = new DataTable();
                dtConv.Columns.Add("DisplayName", typeof(string));
                dtConv.Columns.Add("Id", typeof(string));
                foreach (Microsoft.Lync.Model.Conversation.Conversation c in _ConversationMgr.Conversations)
                {
                    string DisplayName = "";

                    foreach (Participant p in c.Participants)
                    {
                        if (!p.IsSelf)
                        {
                            DisplayName += ", " + p.Properties[ParticipantProperty.Name].ToString();
                            // conbox1.Items.Add(p.Properties[ParticipantProperty.Name].ToString());
                        }
                    }
                    if (DisplayName.Length > 3)
                    {
                        DisplayName = DisplayName.Substring(2);
                    }
                    else
                    {
                        DisplayName = "My Self";
                    }

                    dtConv.Rows.Add(DisplayName, c.Properties[ConversationProperty.Id].ToString());
                    
                    //conbox1.Items.Add(c.Properties[ConversationProperty.Id].ToString());
                }
                conbox1.DataSource = dtConv;
                conbox1.DisplayMember = "DisplayName";
                conbox1.ValueMember = "Id";
                needUpdated = true;
                //datagv1.DataBindings();
                datagv1.DataSource = bindingSource1;
                //conbox1.Update();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
         }

        private void ConversationRemoved(object sender, ConversationManagerEventArgs e)
        {
            
            Conversation c = e.Conversation;
            //foreach(DataRow row in dtConv.Rows)
            for(int i=0;i<dtConv.Rows.Count;i++)
            {
                DataRow row = dtConv.Rows[i];
                if(row["Id"].ToString() == c.Properties[ConversationProperty.Id].ToString())
                {
                    dtConv.Rows.Remove(row);
                }
            }
            needUpdated = true;
            dtConv.AcceptChanges();
           
        }

        private void ConversationAdd(object sender, ConversationManagerEventArgs e)
        {
            string DisplayName = "";
            Conversation c = e.Conversation;

            foreach (Participant p in c.Participants)
            {
                if (!p.IsSelf)
                {
                    DisplayName += ", " + p.Properties[ParticipantProperty.Name].ToString();
                    // conbox1.Items.Add(p.Properties[ParticipantProperty.Name].ToString());
                }
            }
            if (DisplayName.Length > 3)
            {
                DisplayName = DisplayName.Substring(2);
            }
            else
            {
                DisplayName = "My Self";
            }

            dtConv.Rows.Add(DisplayName, c.Properties[ConversationProperty.Id].ToString());
            dtConv.AcceptChanges();
            needUpdated = true;

            //dtConv.Rows.Add();
        }

        private void conbox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(conbox1.SelectedValue.ToString());
              
        }

        private string GetNow()
        {
            String strFileName = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            return strFileName;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (conbox1.SelectedIndex==-1)
            {
                MessageBox.Show("Please select Any Conversation first");
                return;
            }
            String ConId = conbox1.SelectedValue.ToString();
            //dtConv.Columns.Add("Id", typeof(string));
            dtPar = new DataTable();
            dtPar.Columns.Add("Displayname", typeof(string));
            dtPar.Columns.Add("IsAnonymous", typeof(string));
            dtPar.Columns.Add("JoinTime", typeof(string));
            dtPar.Columns.Add("LeftTime", typeof(string));
            isExported = false;
            foreach (Microsoft.Lync.Model.Conversation.Conversation c in _ConversationMgr.Conversations)
            {
                if(c.Properties[ConversationProperty.Id].ToString() == ConId)
                {
                    _LycConversation = c;
                    _LycConversation.ParticipantAdded += ParticipantAdd;
                    _LycConversation.ParticipantRemoved += ParticipantRemoved;
                    foreach (Participant p in c.Participants)
                    {
                        if((Boolean)p.Properties[ParticipantProperty.IsAuthenticated])
                        dtPar.Rows.Add(p.Properties[ParticipantProperty.Name].ToString(), "No", GetNow(), "");
                        else
                            dtPar.Rows.Add(p.Properties[ParticipantProperty.Name].ToString(), "Yes", GetNow(), "");
                    }

                }
            }
            //datagv1.BindingContext(dtPar);
            bindingSource1.DataSource = dtPar;
            datagv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            button1.Enabled = false;
            button2.Enabled = true;

          }
        

        private void ParticipantRemoved(object sender, ParticipantCollectionChangedEventArgs e)
        {
            foreach (DataRow dr in dtPar.Rows)
            {
                if (dr["Displayname"].ToString() == e.Participant.Properties[ParticipantProperty.Name].ToString())
                {
                    dr["LeftTime"] = GetNow();
                    //Console.WriteLine("Left Time:%s", dr["LeftTime"]);
                }
            }

            dtPar.AcceptChanges();
            needUpdated = true;
            //dtPar.
        }

        private void ParticipantAdd(object sender, ParticipantCollectionChangedEventArgs e)
        {
            //e.Participant
            Participant p = e.Participant;

            foreach (DataRow dr in dtPar.Rows)
            {
                if (dr["Displayname"].ToString() == p.Properties[ParticipantProperty.Name].ToString())
                {
                    dr["JoinTime"] = GetNow();
                    dtPar.AcceptChanges();
                    bindingSource1.DataSource = dtPar;
                    datagv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    return;
                }
            }
            if ((Boolean)p.Properties[ParticipantProperty.IsAuthenticated])
                dtPar.Rows.Add(p.Properties[ParticipantProperty.Name].ToString(), "No", GetNow(), "");
            else
                dtPar.Rows.Add(p.Properties[ParticipantProperty.Name].ToString(), "Yes", GetNow(), "");

            needUpdated = true;

        }



        private void button2_Click(object sender, EventArgs e)
        {
            if(isExported)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                timer1.Enabled = false;
            }else
            {
                MessageBox.Show("Please export User List Before Stop Listen");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String strName = string.Format("{0:yyyyMMddHHMMss}", DateTime.Now);
            string fpath = "LyncConfUserlist" + strName;
            ExtractDataToCSV(datagv1);
        }
        private void ExtractDataToCSV(DataGridView dgv)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].Name + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                    isExported = true;
                    MessageBox.Show("CSV file saved.");
                }
            }
            else
            {
                MessageBox.Show("You Didn't Saved List");
            }
            // Confirm to the user it has been completed.
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (needUpdated)
            {
                conbox1.DataSource = null;
                conbox1.Items.Clear();
                conbox1.DataSource = dtConv;
                conbox1.DisplayMember = "DisplayName";
                conbox1.ValueMember = "Id";
                datagv1.Update();
                datagv1.Refresh();
                needUpdated = false;
            }
           
        }
    }
}
