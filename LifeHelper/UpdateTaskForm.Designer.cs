namespace LifeHelper
{
    partial class UpdateTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.cboOngoing = new System.Windows.Forms.CheckBox();
            this.cboDueDateEnforced = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDueDate = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboComplete = new System.Windows.Forms.CheckBox();
            this.cboDismissed = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(84, 449);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(114, 60);
            this.btnSubmit.TabIndex = 19;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "Comments";
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(84, 352);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(589, 26);
            this.txtTaskDescription.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Task Name";
            // 
            // txtTaskName
            // 
            this.txtTaskName.Location = new System.Drawing.Point(84, 251);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(589, 26);
            this.txtTaskName.TabIndex = 15;
            // 
            // cboOngoing
            // 
            this.cboOngoing.AutoSize = true;
            this.cboOngoing.Location = new System.Drawing.Point(84, 148);
            this.cboOngoing.Name = "cboOngoing";
            this.cboOngoing.Size = new System.Drawing.Size(88, 24);
            this.cboOngoing.TabIndex = 14;
            this.cboOngoing.Text = "Ongoing";
            this.cboOngoing.UseVisualStyleBackColor = true;
            // 
            // cboDueDateEnforced
            // 
            this.cboDueDateEnforced.AutoSize = true;
            this.cboDueDateEnforced.Location = new System.Drawing.Point(84, 105);
            this.cboDueDateEnforced.Name = "cboDueDateEnforced";
            this.cboDueDateEnforced.Size = new System.Drawing.Size(166, 24);
            this.cboDueDateEnforced.TabIndex = 13;
            this.cboDueDateEnforced.Text = "Due Date Enforced";
            this.cboDueDateEnforced.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Due Date";
            // 
            // txtDueDate
            // 
            this.txtDueDate.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDueDate.Location = new System.Drawing.Point(84, 55);
            this.txtDueDate.Name = "txtDueDate";
            this.txtDueDate.Size = new System.Drawing.Size(107, 26);
            this.txtDueDate.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(671, 449);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(114, 60);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel and Return";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboComplete
            // 
            this.cboComplete.AutoSize = true;
            this.cboComplete.Location = new System.Drawing.Point(503, 105);
            this.cboComplete.Name = "cboComplete";
            this.cboComplete.Size = new System.Drawing.Size(96, 24);
            this.cboComplete.TabIndex = 20;
            this.cboComplete.Text = "Complete";
            this.cboComplete.UseVisualStyleBackColor = true;
            // 
            // cboDismissed
            // 
            this.cboDismissed.AutoSize = true;
            this.cboDismissed.Location = new System.Drawing.Point(503, 148);
            this.cboDismissed.Name = "cboDismissed";
            this.cboDismissed.Size = new System.Drawing.Size(101, 24);
            this.cboDismissed.TabIndex = 21;
            this.cboDismissed.Text = "Dismissed";
            this.cboDismissed.UseVisualStyleBackColor = true;
            // 
            // UpdateTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 521);
            this.Controls.Add(this.cboDismissed);
            this.Controls.Add(this.cboComplete);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTaskDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.cboOngoing);
            this.Controls.Add(this.cboDueDateEnforced);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDueDate);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UpdateTaskForm";
            this.Text = "UpdateTaskForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateTaskForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaskDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.CheckBox cboOngoing;
        private System.Windows.Forms.CheckBox cboDueDateEnforced;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDueDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cboComplete;
        private System.Windows.Forms.CheckBox cboDismissed;
    }
}