using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using threads = System.Threading.Tasks;

namespace JobLogger.DbMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbMigratorContext migrator = new DbMigratorContext())
            {
                LoadInitialData(migrator);
            }

            Console.Write("Done. Press any key to exit");
            Console.ReadKey();
        }

        private static bool CreateDatabase(DbMigratorContext db)
        {
            try
            {
                Console.WriteLine("Dropping database");
                db.Database.EnsureDeleted();
                Console.WriteLine("Creating database");
                db.Database.EnsureCreated();

                Console.WriteLine("All good");
                Console.WriteLine();

                threads.Task.Delay(500).Wait();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong!!");
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine();

                return false;
            }

        }

        private static void LoadInitialData(DbMigratorContext db)
        {
            LoadCodeBranches(db);
            LoadFeatures(db);
            LoadRequirementTasks(db);
            LoadBugsAndTasks(db);
            LoadOrphanCheckins(db);
            LoadTaskLogsJan(db);
            LoadTaskLogsFeb(db);
            LoadTaskLogsMar(db);
            LoadTaskLogsApr(db);
            LoadTaskLogsMay(db);
            LoadTaskLogsJune(db);
        }

        private static void LoadCodeBranches(DbMigratorContext db)
        {
            Console.Write("Loading Code Branches");

            string[] branches = new string[] { "Dev", "DevNext", "DevNextNext", "Release 4.8" };

            CodeBranchBF bf = new CodeBranchBF(db);

            foreach (string branch in branches)
            {
                Console.Write(".");
                bf.Create(new CodeBranch { Name = branch });
            }

            Console.WriteLine("Done");
            Console.WriteLine();

            threads.Task.Delay(500).Wait();
        }

        private static void LoadFeatures(DbMigratorContext db)
        {
            Console.Write("Loading Features");

            List<Feature> features = new List<Feature>
            {
                new Feature { ID = 114742, Title = "PRODUCT - FS27 Administer - Milestone by Budget Category", Status = RequirementStatus.Closed,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 114743, Title = "PRODUCT - FS27 - Milestone Budget Category backend", Status = RequirementStatus.Closed,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 114824, Title = "Implement milestone Budget Category backend", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 114826, Title = "PRODUCT - FS27 - Add new milestone with category breakdown", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 114827, Title = "Implement add new milestone with category breakdown", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 116747, Title = "Implement milestone budget category logic", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 119328, Title = "Unit testing add milestones", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 116713, Title = "PRODUCT - FS27 - Milestone creation in application transfer", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 116714, Title = "Implement milestone creation in application transfer logic", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 116715, Title = "PRODUCT - FS27 - Changes to application transfer wizard", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 116716, Title = "Implement changes to application transfer wizard", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 116731, Title = "PRODUCT - FS27 - Milestone budget category data update", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 116733, Title = "Implement milestone budget category data migration", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 116727, Title = "PRODUCT - Project original milestone data grid", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 116729, Title = "Implement project original milestone data grid [27]", TaskType = TaskType.Task, IsActive = false }
                            }
                        }
                    }
                },
                new Feature { ID = 121490, Title = "P&C Foundational Model (External Portal Display)", Status = RequirementStatus.Proposed,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 121492, Title = "REBUS - 188 Foundational Model #3", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 122971, Title = "UoM: Verify changes / make changes as required REBUS-188 Foundational Model #3", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 123653, Title = "UoM: Remove Round Stages and Rounds that have been migrated in error", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 123667, Title = "UoM: Create data migration to create 'Idea' round and round stage", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 123674, Title = "UoM: Create blank Infiniti form for Idea (UOM_Idea)", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 123675, Title = "UoM: Create new generic form for IP Disclosure (UOM_IP_Disclosure)", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 124389, Title = "REBUS-600 P & C", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 124390, Title = "Build REBUS-600", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 129987, Title = "REBUS-1058 - Automatically Populate Lead or Administering Department and Owning Academic Division", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 130018, Title = "Initial Developer Analysis", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 130114, Title = "Develop datasources for Infiniti to list linked organisations and departments to contact", TaskType = TaskType.Task, IsActive = false,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "Need to add new controller in ClientExtensions.Uom"},
                                        new TaskComment { Comment = "make some notes about route, classes add!!!"},
                                        new TaskComment { Comment = "Need to add a new DataSource to Infiniti. It is of type 'REST'"}
                                    }
                                }
                            }
                        }
                    }
                },
                new Feature { ID = 121501, Title = "P&C Costing & Pricing (BUDGET)", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 121503, Title = "REBUS - 194 General Details - Create Proposal Budget", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 122532, Title = "UoM : Add classifications 'Research type', 'Funder', 'Multi Institution Agreement'", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 122925, Title = "Uom: Add Contact Types 'Research support officer'", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 122926, Title = "Uom: Add Organisation Types 'Department', 'Administering organisation', 'Faculty', 'Owning department', 'Owning faculty'", TaskType = TaskType.Task, IsActive = false }
                            }
                        }
                    }
                },
                new Feature { ID = 121502, Title = "P&C Workflow", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 125280, Title = "REBUS-419 - WF - Execute Contract Part 2 - Contract Executed", Status = RequirementStatus.Resolved,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 125282, Title = "Build workflow for Execute Contract", TaskType = TaskType.Task, IsActive = false }
                            }
                        },
                        new Requirement { ID = 132511, Title = "REBUS-1072 WF - Project Variations Part 2: Variation Required", Status = RequirementStatus.Proposed,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 132512, Title = "REBUS-1072 WF - Project Variations Part 2: Variation Required", TaskType = TaskType.Task, IsActive = true,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "Created two new project-tokens to accomodate the email - ProjectInternalLink and ProjectRelatedDocumentsInternalLink" }
                                    }
                                }
                            }
                        }
                    }
                },
                new Feature { ID = 123260, Title = "Misc", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 130770, Title = "REBUS-1063 Add a Summary of Contracts into the Application Viewer", Status = RequirementStatus.Active,
                            Comments = new List<RequirementComment>
                            {
                                new RequirementComment { Comment = "Need to add menu tab to Application Viewer (external portel) for Contracts. As a starting point this will a copy of the Documents page, but will filter out not-contract documents prior to displaying them" },
                                new RequirementComment { Comment = "The Application Viewer is access by viewing the 'Applications' page, and clicking the 3 elipses to the left of the application, and clicking 'Application Information" },
                                new RequirementComment { Comment = "Adding a menu tab is achieved by editing the appropriate record in db table 'Layout'. For this task we are interested in the record for 'ApplicationMenu'" },
                                new RequirementComment { Comment = "This tab and 'view' are specific for University of Melbourne, so the new view must be created in ClientExtensions.Uom project" }
                            },
                            Tasks = new List<Task>
                            {
                                new Task { ID = 130771, Title = "REBUS-1063 Add a Summary of Contracts into the Application Viewer", TaskType = TaskType.Task, IsActive = true,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "Step 1. Create a migration to update the 'ApplicationMenu' record in table 'Layout'" },
                                        new TaskComment { Comment = "Step 2. Create a new page 'ApplicationContracts.cshtml' which is placed in 'CustomViews\\Views\\Application' folder" },
                                        new TaskComment { Comment = "Step 3. The new page MUST be configured for 'Build Action = Embedded Resource'"},
                                        new TaskComment { Comment = "Step 4. Need to edit CustomRoutes.csv. We need to add a route for the new page"},
                                        new TaskComment { Comment = "Because we are creating a client specific page that is pretty much a copy of the existing document page, but will have some extra filtering on it. So we don't need to create a new controller method for it, we will just use the custom route to intercept the call, run it through the regular controller method [used for displaying the documents] and then re-direct to the new page that exists in the extensions assembly for UoM" },
                                        new TaskComment { Comment = "The action that is called by clicking the new 'Contracts' tab is Application/ApplicationContracts. By using the custom route definition, we can actually use the ApplicationDocuments controller method to get the list of documents, and then redirect to our new custom page, which will apply some extra filtering to just list contracts" },
                                        new TaskComment { Comment = "This works because the new page is basically showing the same data as the documents page, just with some extra filtering applied" },
                                        new TaskComment { Comment = "This is how the custom route works. For this task, we added two very similar lines; the first additional line is to handle clicking the 'Documents' tab, the second to handle clicking the 'Contracts' tab" },
                                        new TaskComment { Comment = "The first line is 'Application,ApplicationDocuments,ApplicationDocuments,~/Areas/OmniNet/Views/Application/ApplicationDocuments.cshtml'. The first part refers to the controller [Application]. The second part is the actual actual controller method that will be called [ApplicationDocuments]. The third part refers to the action that the button click wants to execute [ApplicationDocuments]. The last part is the address of the page to load [ApplicationDocuments.cshtml - the original page" },
                                        new TaskComment { Comment = "The second line is 'Application,ApplicationDocuments,ApplicationContracts,~/Areas/OmniNet/Views/Application/ApplicationContracts.cshtml'. The first two parts are exactly the same as for the first line. The third part is the action that the button click was trying to call [ApplicationContracts]; this method does not actually exist, but the second part just forces the ApplicationDocuments mehtod to be called instead. The last part is the address of the page to load [ApplicationContracts] which is where the request will be re-directed to. Note that there is no reference to Uom as such, but rember this page is embedded into an assembly that has been loaded for Uom" }
                                    }
                                },
                                new Task { ID = 131860, Title = "External portal - Application - Application viewer: The Edit form button and the Status column are missing in the Contract page", TaskType = TaskType.Bug, IsActive = true,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "For some reason in this instance the custom routes are not working in Test 1 and Test 3, although they work ok in my dev environment"}
                                    }
                                },
                                new Task { ID = 131862, Title = "External portal - Application - Application viewer: The Dowload button is not working in the Contract page", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 131865, Title = "External portal - Application - Application viewer: The alert messages should be displayed correctly in the contract page", TaskType = TaskType.Bug, IsActive = true }
                            }
                        }
                    }
                },
                new Feature { ID = 126505, Title = "P&C Milestones & Deliverables", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 126634, Title = "REBUS-874 Deliverable (Part 1 of 4) base form fields", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 126962, Title = "Add Migrations for configuration of Milestone Types and Custom Properties", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 127774, Title = "REBUS-1031 : Milestone Variance Wizard throws exception", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 128012, Title = "REBUS-1030 : Milestone Custom Properties not being saved", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 128133, Title = "REBUS-1032 : Should not be able to have dupicate Deliverable Numbers for the same project", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 129495, Title = "REBUS-1054 : Cannot vary due date of milestone to last day of project", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 129596, Title = "REBUS-1032 : Internal - Deliverable - Custom properties: The duplicate deliverable numbers should not be saved when navigating to the other page", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 129599, Title = "REBUS-1032 : Internal - Deliverable - Custom properties: The error exception will be occured when opening a project after the warning message for the duplicate Deliverable Number happens", TaskType = TaskType.Bug, IsActive = false },
                                new Task { ID = 129676, Title = "REBUS-1032 : Internal - Deliverable - Custom properties: The validation for the Deliverabloe Number should not be applied for the delete Deliverable", TaskType = TaskType.Bug, IsActive = false }
                            }
                        },
                        new Requirement { ID = 126636, Title = "REBUS-852 WF Deliverables (Pt 1) - technical and financial report", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 130621, Title = "REBUS-1121 : WF Deliverables (Pt 1) - Reminder and Escalation Emails - Provide a hyperlink that takes to the deliverable record", TaskType = TaskType.Bug, IsActive = false,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "Need new migration to update body field in TemplateEmail table"}
                                    }
                                }
                            }
                        },
                        new Requirement { ID = 132251, Title = "REBUS-1226 Research workspace is displayed on the deliverable page", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 132252, Title = "Fix side menu heading on milestone viewer", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 133220, Title = "Help text not correct for Research Project in external portal", TaskType = TaskType.Bug, IsActive = true}
                            }
                        }
                    }
                },
                new Feature { ID = 126507, Title = "P&C Profile", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 127190, Title = "REBUS-1014 FP - Costing & Pricing (Funder Profile Thin Slice)", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 127260, Title = "Initial Developer Analysis", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 127299, Title = "Develop migration to set up required data", TaskType = TaskType.Task, IsActive = true }
                            }
                        }
                    }
                },
                new Feature { ID = 126515, Title = "P&C Variations", Status = RequirementStatus.Proposed,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 132351, Title = "REBUS-567 WF - Project Variations Part 1: Variation Required", Status = RequirementStatus.Active,
                            Comments = new List<RequirementComment>
                            {
                                new RequirementComment { Comment = "Need to add a form to accept a reason for the variation, plus 'plumb' the from into the external portal, and lastly a workflow to handle the transitions" }
                            },
                            Tasks = new List<Task>
                            {
                                new Task
                                {
                                    ID = 132355, Title = "Create new form for REBUS-567", TaskType = TaskType.Task, IsActive = true,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "I used UOM Project Closure Checklist as a bit of a guide for creating the form. Note that I had to add a Template control to the finish page. To this end I downloaded 'Generic Form' from UOM Project Closure Checklist's Finish page, and then used the downloaded file to add 'Generic Form' to my new form" }
                                    }
                                },
                                new Task { ID = 132356, Title = "Enable the use of the new form for REBUS-567", TaskType = TaskType.Task, IsActive = true },
                                new Task { ID = 132357, Title = "Create workflow to respond to project variation for REBUS-567", TaskType = TaskType.Task, IsActive = true }
                            }
                        }
                    }
                },
                new Feature { ID = 126795, Title = "P&C ServiceNow", Status = RequirementStatus.Proposed,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 127189, Title = "REBUS-949 Part 1a: ServiceNow View in Omnistar", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 130719, Title = "REBUS-949 Part 1a: ServiceNow View in Omnistar", TaskType = TaskType.Task, IsActive = true,
                                    Comments = new List<TaskComment>
                                    {
                                        new TaskComment { Comment = "Quite similar to REBUS-1063 [Add a Summary of Contracts into the Application Viewer"},
                                        new TaskComment { Comment = "Will add a tab called 'ServiceNow'. Also add new page. But may need to add a controller, because the data model for the new page will be new"}
                                    }
                                }
                            }
                            
                        }
                    }
                }
            };

            FeatureBF bf = new FeatureBF(db);
            foreach (Feature feature in features)
            {
                Console.Write(".");
                bf.Create(feature);
            }

            Console.WriteLine("Done");
            Console.WriteLine();

            threads.Task.Delay(500).Wait();
        }

        private static void LoadRequirementTasks(DbMigratorContext db)
        {
            Console.Write("Loading Requirements and Tasks");

            List<Requirement> requirements = new List<Requirement>
            {
                new Requirement { ID = 124838, Title = "REBUS-427 Approvals Part 2 - Approve / Reject Execute Contract", Status = RequirementStatus.Resolved,
                    Tasks = new List<Task>
                    {
                        new Task { ID = 124839, Title = "Build workflow to transition application from status \"Waiting Approval: Execute Contract\"", TaskType = TaskType.Task, IsActive = false }
                    } },
                new Requirement { ID = 125288, Title = "REBUS-199 WF - Project Establishment Part 1: Setup", Status = RequirementStatus.Resolved,
                    Tasks = new List<Task>
                    {
                        new Task { ID = 125290, Title = "Add new Project Status records", TaskType = TaskType.Task, IsActive = false }
                    } },
                new Requirement { ID = 126755, Title = "REBUS-938 - TF - Milestone types for US-PC-PST007", Status = RequirementStatus.Resolved,
                    Tasks = new List<Task>
                    {
                        new Task { ID = 126760, Title = "Add migration for milestone types", TaskType = TaskType.Task, IsActive = false }
                    }
                },
            };

            RequirementBF bf = new RequirementBF(db);
            foreach (Requirement requirement in requirements)
            {
                Console.Write(".");
                bf.Create(requirement);
            }

            Console.WriteLine("Done");
            Console.WriteLine();

            threads.Task.Delay(500).Wait();
        }

        private static void LoadBugsAndTasks(DbMigratorContext db)
        {
            Console.Write("Loading Bugs");

            List<Task> tasks = new List<Task>
            {
                new Task { ID = 93976, Title = "External - Applications - Application Details: History tables in OmniNet are incorrect", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 108437, Title = "Internal - External management - Assessment types: All the assessment types have their select all application documents toggles set to no", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 112925, Title = "Internal - Data migration - Reporting database export: The Project organisation start & end date columns remain visible after disabling the feature", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 113757, Title = "Internal - Grants - Financial summary: The financial summary graph is displaying the paid and unpad amount on seperate rows", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 114032, Title = "Internal - Milestones - Process milestone: A blank email is sent to a user when the milestone process is triggered with a received status", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 115935, Title = "Internal - Milestones - Process milestone wizard: Reopening a process milestone wizard  that's been edited after undoing it doesn't display the restore previous session option", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 116503, Title = "Internal - Milestones - Process milestone: The create debtor transactionstep doesn't appear in the step list when the not achieved status is selected", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 117910, Title = "Internal - Milestones - Process invoice: Cannot process invoice an income milestone that is partially invoiced", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 117940, Title = "Internal - Grants - Milestones: Draining a milestone reduces the amount to zero regardless of what's in the invoiced amount", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 117972, Title = "Internal - Grants - Milestones: Undoing a drained milestone leaves the amount as zero", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 118102, Title = "Internal - Grants - Milestones: Editing a recovery milestone's before tax amount doesn't work properly", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 118914, Title = "External - Advanced search filter: Cannot open the advanced search filter", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 118993, Title = "Internal - Applications/Grants: The edit button needs to be clicked twice", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 124006, Title = "External - Research Projects: In Ethics Clearance page in the Project Record, the Upload document button should be displayed", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 124059, Title = "External - Research Projects: In the Details page in the Project Records, the \"Is this Research Project confidential\" field should has the text field first and the check box in the same line", TaskType = TaskType.Bug, IsActive = false },
                new Task { ID = 132284, Title = "REBUS-1239 Rename application output document in a way that is nicer for output (eg \"Rebus application details\")", TaskType = TaskType.Task, IsActive = false },
                new Task { ID = 132285, Title = "REBUS-1256 'Successful' and 'Unsuccessful' omninet-visible statuses", TaskType = TaskType.Task, IsActive = true,
                    Comments = new List<TaskComment>
                    {
                        new TaskComment { Comment = "New data migration to update value of IsVisible to TRUE for status 'Successful' and 'Unsuccessful'"}
                    }
                },
                new Task { ID = 132262, Title = "REBUS-1233 Investigate having the same project ID as the application it was transferred from", TaskType = TaskType.Bug, IsActive = true },
                new Task { ID = 133294, Title = "External - Project - Deliverable: The page description should be displayed with the correct information", TaskType = TaskType.Bug, IsActive = true },
                new Task { ID = 133375, Title = "Internal - Project: Cannot create a new project through the wizard steps", TaskType = TaskType.Bug, IsActive = true }
            };

            TaskBF bf = new TaskBF(db);
            foreach (Task task in tasks)
            {
                Console.Write(".");
                bf.Create(task);
            }

            Console.WriteLine("Done");
            Console.WriteLine();

            threads.Task.Delay(500).Wait();
        }

        private static void LoadOrphanCheckins(DbMigratorContext db)
        {
            Console.Write("Loading orphaned CheckIns");

            List<CheckIn> checkIns = new List<CheckIn>
            {
                new CheckIn { ID = 68208, CheckInTime = DateTime.Parse("08-May-2018 13:49"), Comment = "Fix (Login): allow progress through the browser detection even when browser is not officially supported", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68222, CheckInTime = DateTime.Parse("08-May-2018 16:14"), Comment = "Fix (System Settings): data collection member descriptions now can be extended to 50 characters", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68239, CheckInTime = DateTime.Parse("09-May-2018 09:45"), Comment = "Fix (Login): use .prop instead of .attr and .removeAttr", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68343, CheckInTime = DateTime.Parse("14-May-2018 12:07"), Comment = "Fix (Default Statuses): select control now syncs properly with Default Status selector", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68394, CheckInTime = DateTime.Parse("15-May-2018 15:22"), Comment = "It is now possible to create a project abbreviation with 2, 3 or 4 characters", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68453, CheckInTime = DateTime.Parse("17-May-2018 13:45"), Comment = "Fix (HomeScreen widgets): Items in widgets now disappear when application or project is deleted", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68559, CheckInTime = DateTime.Parse("22-May-2018 10:01"), Comment = "Fix (System Settings): Now able to display up to 15 eligibility questions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68595, CheckInTime = DateTime.Parse("22-May-2018 16:46"), Comment = "Fix (System Settings): Original fix has broken other areas. Backed out changes", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68597, CheckInTime = DateTime.Parse("22-May-2018 17:43"), Comment = "Fix (System Settings): Now works properly", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68847, CheckInTime = DateTime.Parse("31-May-2018 13:31"), Comment = "Feat (Reports): New reports for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68916, CheckInTime = DateTime.Parse("01-Jun-2018 15:27"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68917, CheckInTime = DateTime.Parse("01-Jun-2018 15:28"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68918, CheckInTime = DateTime.Parse("01-Jun-2018 15:29"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68919, CheckInTime = DateTime.Parse("01-Jun-2018 15:31"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68920, CheckInTime = DateTime.Parse("01-Jun-2018 15:32"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68921, CheckInTime = DateTime.Parse("01-Jun-2018 15:32"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68922, CheckInTime = DateTime.Parse("01-Jun-2018 15:33"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68923, CheckInTime = DateTime.Parse("01-Jun-2018 15:34"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68924, CheckInTime = DateTime.Parse("01-Jun-2018 15:35"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68925, CheckInTime = DateTime.Parse("01-Jun-2018 15:35"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68926, CheckInTime = DateTime.Parse("01-Jun-2018 15:37"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68978, CheckInTime = DateTime.Parse("04-Jun-2018 15:55"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 68979, CheckInTime = DateTime.Parse("04-Jun-2018 15:58"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69309, CheckInTime = DateTime.Parse("13-Jun-2018 16:16"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69310, CheckInTime = DateTime.Parse("13-Jun-2018 16:20"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69312, CheckInTime = DateTime.Parse("13-Jun-2018 16:22"), Comment = "Feat (Reports): New report for OEH", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69351, CheckInTime = DateTime.Parse("14-Jun-2018 12:15"), Comment = "Fix (Letter Templates): Added templates for successful and unsuccessful applications", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69365, CheckInTime = DateTime.Parse("14-Jun-2018 14:54"), Comment = "Feat (Approvals): New Milestone Payment Recommendation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69372, CheckInTime = DateTime.Parse("14-Jun-2018 16:10"), Comment = "Feat (Reports): New user reports - minor fix for when no data retreived", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69425, CheckInTime = DateTime.Parse("15-Jun-2018 16:06"), Comment = "Feat (Reports): fixes after review", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69535, CheckInTime = DateTime.Parse("19-Jun-2018 15:57"), Comment = "Feat(approvals): Approval Type edit form to cater for new fields", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69564, CheckInTime = DateTime.Parse("20-Jun-2018 09:46"), Comment = "feat(approvals): Fix to prevent compile error", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69571, CheckInTime = DateTime.Parse("20-Jun-2018 11:17"), Comment = "fix(navmenu): fix unfriendly text (no spaces between words) in history", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69582, CheckInTime = DateTime.Parse("20-Jun-2018 12:29"), Comment = "fix(letter templates): remove template type", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69610, CheckInTime = DateTime.Parse("20-Jun-2018 15:58"), Comment = "fix(application history): fix date sorting in table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID,
                    TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {TaskID = 93976 } } },
                new CheckIn { ID = 69696, CheckInTime = DateTime.Parse("22-Jun-2018 10:42"), Comment = "feat (global doc searce): Add Document Tag filter", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69715, CheckInTime = DateTime.Parse("22-Jun-2018 15:04"), Comment = "fix(panel view): Fix hint text on 'view applicaiton' button. Also fix to TaskValidationAtrrtibute to prevent intermitten...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69745, CheckInTime = DateTime.Parse("25-Jun-2018 09:23"), Comment = "fix(meetings): change application to project in empty list message", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69770, CheckInTime = DateTime.Parse("25-Jun-2018 16:39"), Comment = "fix(panel view): Fix hint text on 'view project' button. ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69790, CheckInTime = DateTime.Parse("26-Jun-2018 11:25"), Comment = "fix (meeting): bulk download of meeting documents now checks the Active property of the Merge Document Order object", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69802, CheckInTime = DateTime.Parse("26-Jun-2018 15:02"), Comment = "fix (application timeline): prevent exception when application review exists but user is unable to see them", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69839, CheckInTime = DateTime.Parse("27-Jun-2018 12:41"), Comment = "fix (Decisions): fix missing string resource ViewUserDecision", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69884, CheckInTime = DateTime.Parse("28-Jun-2018 10:49"), Comment = "fix(reports): clicking favourite button from project viewer : reports no longer causes exception", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 69901, CheckInTime = DateTime.Parse("28-Jun-2018 13:37"), Comment = "fix (data migration): CSV export of reporting db table list now works", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70046, CheckInTime = DateTime.Parse("03-Jul-2018 19:29"), Comment = "fix (milestone): Now able to provide milestone amount when changing milestone type to a type with payments", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70062, CheckInTime = DateTime.Parse("04-Jul-2018 11:53"), Comment = "fix (mileston): replaced == with ===", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70081, CheckInTime = DateTime.Parse("04-Jul-2018 16:41"), Comment = "feat (meeting): migration to add EmailSent flag to Meeting tables", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70147, CheckInTime = DateTime.Parse("06-Jul-2018 09:52"), Comment = "feat (meetings) : implementing Email Sent icon on meeting details view", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70157, CheckInTime = DateTime.Parse("06-Jul-2018 11:34"), Comment = "feat (meetings) : add email sent flag to meeting tables in reporting database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70179, CheckInTime = DateTime.Parse("06-Jul-2018 15:18"), Comment = "feat (email templates): add 5 new templates", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70186, CheckInTime = DateTime.Parse("06-Jul-2018 16:51"), Comment = "feat (email templates): removed inserts from old over-used migration into their own migration", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID },
                new CheckIn { ID = 70203, CheckInTime = DateTime.Parse("09-Jul-2018 09:55"), Comment = "feat (meetings) : implementing Email Sent icon on meeting details view", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70206, CheckInTime = DateTime.Parse("09-Jul-2018 10:05"), Comment = "feat (meetings) : add email sent flag to meeting tables in reporting database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70209, CheckInTime = DateTime.Parse("09-Jul-2018 10:31"), Comment = "feat (meetings) : add email sent flag to meeting tables in reporting database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70257, CheckInTime = DateTime.Parse("10-Jul-2018 12:51"), Comment = "feat (email templates): add links between email template and approval types", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70330, CheckInTime = DateTime.Parse("11-Jul-2018 15:32"), Comment = "feat (meeting type): add Generic Form Default to Meeting Type edit page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70332, CheckInTime = DateTime.Parse("11-Jul-2018 15:39"), Comment = "feat (meeting type): changes to meetingType db table to cater for generic forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70333, CheckInTime = DateTime.Parse("11-Jul-2018 15:46"), Comment = "feat (meeting types): business logic to cater for generic form defaults in meeting type", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70334, CheckInTime = DateTime.Parse("11-Jul-2018 15:53"), Comment = "feat (email templates): re-did migration to check for existing records", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70360, CheckInTime = DateTime.Parse("12-Jul-2018 11:59"), Comment = "feat (generic form): add MeetingGenericForm table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70373, CheckInTime = DateTime.Parse("12-Jul-2018 14:53"), Comment = "feat (generic form): fix feature.xml to prevent duplicate item error", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70522, CheckInTime = DateTime.Parse("19-Jul-2018 10:36"), Comment = "feat (meeting): add generic forms to meeting details", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70532, CheckInTime = DateTime.Parse("19-Jul-2018 16:16"), Comment = "feat (meeting): added project id and application id fields to meeting generic form model to make viewing the parent (app...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70537, CheckInTime = DateTime.Parse("19-Jul-2018 18:32"), Comment = "feat (meeting): generic form approval now saved", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70591, CheckInTime = DateTime.Parse("23-Jul-2018 10:12"), Comment = "feat (meeting): maintain generic form attachments", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70602, CheckInTime = DateTime.Parse("23-Jul-2018 14:41"), Comment = "feat (meetings): Add email for generic forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70623, CheckInTime = DateTime.Parse("24-Jul-2018 09:18"), Comment = "feat (meetings): Missed checking in project file", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70648, CheckInTime = DateTime.Parse("24-Jul-2018 13:05"), Comment = "feat (meeting): Add form lookup filter now only includes Generic form types", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70653, CheckInTime = DateTime.Parse("24-Jul-2018 14:45"), Comment = "feat (meeting): Generic form email now selects correct email template", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70670, CheckInTime = DateTime.Parse("25-Jul-2018 08:55"), Comment = "feat (reporting): Add Meeting Generic Form table to reporting database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70701, CheckInTime = DateTime.Parse("25-Jul-2018 14:34"), Comment = "feat (meetings - external): add forms tab to meeting details screen menu", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70759, CheckInTime = DateTime.Parse("26-Jul-2018 16:48"), Comment = "feat (meetings - external portal): add generic form tab to meeting details form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70768, CheckInTime = DateTime.Parse("26-Jul-2018 19:18"), Comment = "feat (meetings): Add ability to download form attachments from meeting details page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70780, CheckInTime = DateTime.Parse("27-Jul-2018 13:11"), Comment = "feat (approvals): Send email button is not visible for a decision unless the outcome has been overridden and that decisi...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70796, CheckInTime = DateTime.Parse("27-Jul-2018 16:03"), Comment = "feat (email templates): Prevent deletion of email template that has been assisnged to an approval type", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70804, CheckInTime = DateTime.Parse("27-Jul-2018 17:15"), Comment = "fix (system settings): fix missing string resources from maintain approval option email templates", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70940, CheckInTime = DateTime.Parse("01-Aug-2018 18:14"), Comment = "fix (related documents): fix tool tip on button for visiblity in related entity", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70948, CheckInTime = DateTime.Parse("02-Aug-2018 09:13"), Comment = "feat (reporting): fix up snapshot stored procedure to remove hardcoded database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 70955, CheckInTime = DateTime.Parse("02-Aug-2018 10:53"), Comment = "fix (eligibility): fixed label", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71030, CheckInTime = DateTime.Parse("06-Aug-2018 08:38"), Comment = "fix (reviews): fix knockoout coding error ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71135, CheckInTime = DateTime.Parse("07-Aug-2018 16:35"), Comment = "fix (application viewer): Default selected documents now set when adding an application to a review from the application...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71169, CheckInTime = DateTime.Parse("08-Aug-2018 14:15"), Comment = "fix(lazy-json): Make json lookup slightly less lazy but still a bit lazy", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71209, CheckInTime = DateTime.Parse("09-Aug-2018 12:40"), Comment = "fix (forms - external): ensure form attachments and related documents are included in download from Application / Forms ...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71240, CheckInTime = DateTime.Parse("10-Aug-2018 10:17"), Comment = "Changeset 71209: fix (forms - external): ensure form attachments and related documents are included in download from App...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71322, CheckInTime = DateTime.Parse("14-Aug-2018 09:42"), Comment = "fix (meetings): ensure that generic forms are downloaded when requested from Meeting Details page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71339, CheckInTime = DateTime.Parse("14-Aug-2018 13:48"), Comment = "fix (decisions): document identifier added to form types in drop down", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71359, CheckInTime = DateTime.Parse("14-Aug-2018 16:09"), Comment = "fix (projects - External): identifier displayed together with document type when listing forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71390, CheckInTime = DateTime.Parse("15-Aug-2018 12:55"), Comment = "fix (reporting): data import was throwing an exception", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71397, CheckInTime = DateTime.Parse("15-Aug-2018 15:01"), Comment = "fix (application viewer): remove Regions menu item from application viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71404, CheckInTime = DateTime.Parse("15-Aug-2018 16:20"), Comment = "fix (dashboard): status filter for forms widget now only inlcludes \"In Progress\" and \"Submitted\"", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71429, CheckInTime = DateTime.Parse("16-Aug-2018 10:31"), Comment = "fix (project viewer): Submitted date of related forms not showing correct time (is being incorrectly treated as UTC time...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71465, CheckInTime = DateTime.Parse("16-Aug-2018 17:01"), Comment = "Fix (Form Dashboard): Correction to query when Approval Pending is selected as filter", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71490, CheckInTime = DateTime.Parse("17-Aug-2018 12:13"), Comment = "fix (application viewer: external): ensure that left menu menu-items are loaded when clicking all tabs when in applicati...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71529, CheckInTime = DateTime.Parse("18-Aug-2018 14:21"), Comment = "fix (meetings): Allow user with REGAdmin permission to add form to a meeting", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71530, CheckInTime = DateTime.Parse("18-Aug-2018 16:48"), Comment = "fix (external): when logging back in with the wrong user with a redirect to a form, now get 404 error, not exception", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71623, CheckInTime = DateTime.Parse("21-Aug-2018 09:34"), Comment = "feat (home): home page widgets now list all records, with paging", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71652, CheckInTime = DateTime.Parse("22-Aug-2018 09:41"), Comment = "feat (home page): add sorting capability to home page dashboard widgets", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71653, CheckInTime = DateTime.Parse("22-Aug-2018 10:33"), Comment = "fix (meeting types): default template letters not being displayed when details are loaded into view", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71657, CheckInTime = DateTime.Parse("22-Aug-2018 11:22"), Comment = "Changeset 71530: fix (external): when logging back in with the wrong user with a redirect to a form, now get 404 error, ...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71733, CheckInTime = DateTime.Parse("24-Aug-2018 13:12"), Comment = "feat (dashboard): added sorting to widgets", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71762, CheckInTime = DateTime.Parse("27-Aug-2018 12:00"), Comment = "feat (decision): Add 'Select All Documents' flag to StatusTemplateMappings", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71866, CheckInTime = DateTime.Parse("29-Aug-2018 18:13"), Comment = "feat (decisions): add application documents to decision details page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 71893, CheckInTime = DateTime.Parse("30-Aug-2018 14:41"), Comment = "fix (internal : applications): filter menu now showing correct terminology for ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71932, CheckInTime = DateTime.Parse("31-Aug-2018 11:10"), Comment = "fix (contacts): view icon on contact details screen now always visible regardless of whether in edit mode or not", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71936, CheckInTime = DateTime.Parse("31-Aug-2018 12:04"), Comment = "fix (contacts): corrected missing space between first and last names for people in the recents list", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 71952, CheckInTime = DateTime.Parse("31-Aug-2018 16:54"), Comment = "fix (contacts): prevent escalating problem with attempting to save new contact positions when bad data has been processe...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72006, CheckInTime = DateTime.Parse("04-Sep-2018 12:12"), Comment = "feat (approval decisions): add new secondary landing page for approval decisions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72016, CheckInTime = DateTime.Parse("04-Sep-2018 14:38"), Comment = "feat (approval bulk): added new features for approval decisions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72036, CheckInTime = DateTime.Parse("05-Sep-2018 09:02"), Comment = "feat (approval actions : external): alter layout of approval action menus to enable hiding of action based on feature", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72037, CheckInTime = DateTime.Parse("05-Sep-2018 09:19"), Comment = "remove errant space in layout config", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72062, CheckInTime = DateTime.Parse("05-Sep-2018 16:15"), Comment = "feat (external - approval action): remove decision from advanced filters, and removed Grant Amount from the details tabl...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72080, CheckInTime = DateTime.Parse("06-Sep-2018 11:02"), Comment = "feat (ProjectApplicationParticipation): Add start and end dates to ProjectApplicationParticipation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72084, CheckInTime = DateTime.Parse("06-Sep-2018 11:49"), Comment = "fix (reporting): corrections to previous change that prevented the creation of the ce database", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72087, CheckInTime = DateTime.Parse("06-Sep-2018 13:12"), Comment = "feat (reporting): add Start and End Date fields to ProjectOrganisation table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72100, CheckInTime = DateTime.Parse("06-Sep-2018 16:30"), Comment = "feat (features): added feature ProjectOrganisationDates", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72131, CheckInTime = DateTime.Parse("07-Sep-2018 14:19"), Comment = "feat (milestones): added token for milestoneUrl and milestoneLink", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72135, CheckInTime = DateTime.Parse("07-Sep-2018 14:56"), Comment = "fix (approval decisions): corrected behaviour on approvals landing page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72181, CheckInTime = DateTime.Parse("10-Sep-2018 14:44"), Comment = "fix (approvals): corrected behaviour of approvals feature, including removing totally the feature ApprovalInBulk, which ...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72222, CheckInTime = DateTime.Parse("11-Sep-2018 12:47"), Comment = "feat (approval types): add new approval type, plus email template", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72224, CheckInTime = DateTime.Parse("11-Sep-2018 13:13"), Comment = "Moving migrations out of product into NHMRC", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72234, CheckInTime = DateTime.Parse("11-Sep-2018 15:07"), Comment = "feat (email template): fixed EmailFooterType= System", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72257, CheckInTime = DateTime.Parse("12-Sep-2018 09:34"), Comment = "fix (approvals): tidy up", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72265, CheckInTime = DateTime.Parse("12-Sep-2018 10:38"), Comment = "feat (workflow): new workflow for create approval - certify milestone for NHMRC acceptance - RAO", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72274, CheckInTime = DateTime.Parse("12-Sep-2018 12:50"), Comment = "feat (workflow): new workflow for create approval - certify milestone for NHMRC acceptance - RAO", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72330, CheckInTime = DateTime.Parse("14-Sep-2018 11:06"), Comment = "feat (workflow): changes required to faciltate the workflow", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72333, CheckInTime = DateTime.Parse("14-Sep-2018 11:30"), Comment = "feat (workflow): new workflow for NHMRC", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72336, CheckInTime = DateTime.Parse("14-Sep-2018 11:35"), Comment = "feat (workflow): add new workflow for NHMRC", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72385, CheckInTime = DateTime.Parse("16-Sep-2018 21:21"), Comment = "feat (workflow): new workflow", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72386, CheckInTime = DateTime.Parse("16-Sep-2018 21:36"), Comment = "feat (workflow): add new workflow", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72395, CheckInTime = DateTime.Parse("17-Sep-2018 09:30"), Comment = "feat (form mappings): add form mappings for scientific progress report and final reports", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72457, CheckInTime = DateTime.Parse("19-Sep-2018 09:32"), Comment = "feat (project details (ext)): New financial summary page in project viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72568, CheckInTime = DateTime.Parse("20-Sep-2018 18:22"), Comment = "feat (project details): new financial summary page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72623, CheckInTime = DateTime.Parse("21-Sep-2018 16:52"), Comment = "feat (project details: ext): develop summary table for financial summary page in project viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72639, CheckInTime = DateTime.Parse("24-Sep-2018 09:55"), Comment = "feat (project viewer - ext): finish toggle for summary or detailed list on summary financial page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72665, CheckInTime = DateTime.Parse("24-Sep-2018 14:51"), Comment = "feat (MilestoneType): add VisibleInOmniNet flag", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72666, CheckInTime = DateTime.Parse("24-Sep-2018 15:19"), Comment = "feat (milestoneType): correct new column name", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72675, CheckInTime = DateTime.Parse("24-Sep-2018 16:14"), Comment = "feat (milestoneType): update UI to enable editing of IsVisibleInExternalPortal field", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72681, CheckInTime = DateTime.Parse("24-Sep-2018 16:38"), Comment = "feat (milestoneType): migrations for reporting database for the new IsVisibleInExternalPortal field", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72718, CheckInTime = DateTime.Parse("25-Sep-2018 13:06"), Comment = "feat (milestones - ext): restrict milestones displayed in project viewer to exclude milestones not meant to be visible i...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72729, CheckInTime = DateTime.Parse("25-Sep-2018 15:33"), Comment = "feat (milestones - ext): alter graphs", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72743, CheckInTime = DateTime.Parse("25-Sep-2018 18:02"), Comment = "fix (related forms): prevent log out when attempting to access related forms in project viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72747, CheckInTime = DateTime.Parse("26-Sep-2018 08:49"), Comment = "feat (milestones - ext): add advanced filter to milestone page in project viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72785, CheckInTime = DateTime.Parse("27-Sep-2018 09:04"), Comment = "feat (organisation): added tokens for organisation start and end dates", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72807, CheckInTime = DateTime.Parse("27-Sep-2018 16:20"), Comment = "feat (project organisation): add filter to organisation details to allow filtering out non-active organisation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72856, CheckInTime = DateTime.Parse("01-Oct-2018 15:27"), Comment = "feat (project organisation): add start date and end date fields to organisation edit, and organisation table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72864, CheckInTime = DateTime.Parse("02-Oct-2018 10:13"), Comment = "feat (project viewer): add organisation start and end date to project summary", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72872, CheckInTime = DateTime.Parse("02-Oct-2018 12:11"), Comment = "feat (project organisation): added date validation to start and end date for primary organisation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72887, CheckInTime = DateTime.Parse("02-Oct-2018 14:01"), Comment = "feat (project - copy): organisation start and end dates captured in project copy ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72896, CheckInTime = DateTime.Parse("02-Oct-2018 14:49"), Comment = "feat (project - organisation): client-side logic to allow multiple primary organisations", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72907, CheckInTime = DateTime.Parse("02-Oct-2018 15:56"), Comment = "fix: spelling mistake", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 72974, CheckInTime = DateTime.Parse("04-Oct-2018 09:25"), Comment = "Fix ordering", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73046, CheckInTime = DateTime.Parse("05-Oct-2018 16:27"), Comment = "Fix label in external portal", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73049, CheckInTime = DateTime.Parse("05-Oct-2018 16:48"), Comment = "feat (workflow): new workflow for Confirm decline of Revised Schedule - PO responded", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73051, CheckInTime = DateTime.Parse("05-Oct-2018 17:28"), Comment = "feat (workflow): new workflow for Confirm decline of Revised Schedule - PO responded", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73074, CheckInTime = DateTime.Parse("08-Oct-2018 16:45"), Comment = "feat (workflow): new workflow for Create Approval - Action revised Grant Schedule: new workflow step to update visibilit...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73145, CheckInTime = DateTime.Parse("10-Oct-2018 10:57"), Comment = "Changeset 73074: feat (workflow): new workflow for Create Approval - Action revised Grant Schedule: ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73150, CheckInTime = DateTime.Parse("10-Oct-2018 12:24"), Comment = "Small fix", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73184, CheckInTime = DateTime.Parse("11-Oct-2018 10:04"), Comment = "feat (workflow): new workflow for 'Create approval Action Grant Offer - RAO'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73205, CheckInTime = DateTime.Parse("11-Oct-2018 14:24"), Comment = "feat (workflow): small fix to workflow step", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73215, CheckInTime = DateTime.Parse("11-Oct-2018 15:40"), Comment = "fix (financial summary): fixed colour of toggle button in external portal", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73220, CheckInTime = DateTime.Parse("11-Oct-2018 16:05"), Comment = "fix (financial summary - external): correctly hide in-kind contributions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73235, CheckInTime = DateTime.Parse("12-Oct-2018 09:48"), Comment = "fix (financial summary): default grouping used on initial page load now", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73238, CheckInTime = DateTime.Parse("12-Oct-2018 10:27"), Comment = "fix (financial summary): Milestone category options not displayed unless that feature is on", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73242, CheckInTime = DateTime.Parse("12-Oct-2018 10:45"), Comment = "fix (financial summary)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73244, CheckInTime = DateTime.Parse("12-Oct-2018 10:52"), Comment = "fix (financial summary): fix labels", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73255, CheckInTime = DateTime.Parse("12-Oct-2018 13:01"), Comment = "fix (financial summary): fix - not show categories when feature is not enabled", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73261, CheckInTime = DateTime.Parse("12-Oct-2018 14:21"), Comment = "fix (milestone type): new milestone type will now have visible in external portal set to true by default", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73284, CheckInTime = DateTime.Parse("15-Oct-2018 11:09"), Comment = "fix (financial summary - external)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73297, CheckInTime = DateTime.Parse("15-Oct-2018 16:08"), Comment = "fix (financial summary - external)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73330, CheckInTime = DateTime.Parse("16-Oct-2018 16:33"), Comment = "fix(reviews): fixes to reviews: display download button correctly", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 73351, CheckInTime = DateTime.Parse("17-Oct-2018 12:57"), Comment = "feat (budget categories): enable budget categoris for nhmrc by default", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73357, CheckInTime = DateTime.Parse("17-Oct-2018 13:47"), Comment = "feat (budget categories): migration to add fields to Budget Category", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73394, CheckInTime = DateTime.Parse("18-Oct-2018 12:16"), Comment = "feat (budget categories): incomplete work on budget category maintenance page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73499, CheckInTime = DateTime.Parse("23-Oct-2018 09:12"), Comment = "feat (Budget Categories): changes to maintenace screen", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73538, CheckInTime = DateTime.Parse("23-Oct-2018 19:06"), Comment = "feat (Budget Categories): partial adding ability to delete budget categories", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73636, CheckInTime = DateTime.Parse("26-Oct-2018 12:03"), Comment = "feat (bugdet categories): implement delete budget category (incomplete)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73641, CheckInTime = DateTime.Parse("26-Oct-2018 13:49"), Comment = "fix (workflow): prevent Grant Offer - RAO action from being created when Grant Schedule document has not been uploaded", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73705, CheckInTime = DateTime.Parse("29-Oct-2018 16:16"), Comment = "fix (approval): clear selected document when changing approval type", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73713, CheckInTime = DateTime.Parse("29-Oct-2018 17:55"), Comment = "fix (budget category): prevent exception when attempting to save new budget category", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73726, CheckInTime = DateTime.Parse("30-Oct-2018 09:26"), Comment = "fix (layouts): fix order of menu tabs in project viewer ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73786, CheckInTime = DateTime.Parse("31-Oct-2018 10:33"), Comment = "fix (approvals): show approval comment in table of approvals", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 73878, CheckInTime = DateTime.Parse("01-Nov-2018 17:00"), Comment = "fix (bulk approvals): can now save decisions after initially canceling and then re-entering the decision", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74037, CheckInTime = DateTime.Parse("07-Nov-2018 12:37"), Comment = "fix (approvals): ensure decision comment is visible in approval decisions table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74043, CheckInTime = DateTime.Parse("07-Nov-2018 13:40"), Comment = "fix (financial summary): correction to format for displaying financial year", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74091, CheckInTime = DateTime.Parse("08-Nov-2018 12:33"), Comment = "fix (milestones): fix issue where when adding two milestones of the same kind at once sets the status of both to not ach...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74093, CheckInTime = DateTime.Parse("08-Nov-2018 12:47"), Comment = "fix (budget category): fix caption for maintain Budget Category page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74096, CheckInTime = DateTime.Parse("08-Nov-2018 13:15"), Comment = "fix (financial summary): correction to format of date", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74100, CheckInTime = DateTime.Parse("08-Nov-2018 13:38"), Comment = "fix (budget category): search on Maintain Budget Category page is now case-insensitive", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74112, CheckInTime = DateTime.Parse("08-Nov-2018 15:46"), Comment = "fix (financial summary): fix labels on chart to only dim text, not make it invisible", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74136, CheckInTime = DateTime.Parse("09-Nov-2018 11:11"), Comment = "fix (financial summary): Payments is now selected by default in advanced filter", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74238, CheckInTime = DateTime.Parse("13-Nov-2018 10:46"), Comment = "feat (workflows): Email now will be sent to sapphire@nhmrc.gov.au when no RAO has been set up for principal organisation...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74248, CheckInTime = DateTime.Parse("13-Nov-2018 11:57"), Comment = "feat (workflows): fix spelling mistake in new template", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74290, CheckInTime = DateTime.Parse("14-Nov-2018 12:50"), Comment = "feat (cash contributions): Immplement toggles for contribution contributer rather than radio buttons", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74343, CheckInTime = DateTime.Parse("15-Nov-2018 12:21"), Comment = "feat (milestone summary): add additional columns to income estimate table (incomplete)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74390, CheckInTime = DateTime.Parse("16-Nov-2018 09:17"), Comment = "feat (milestone summary): add additional columns to income estimate table (incomplete)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74431, CheckInTime = DateTime.Parse("19-Nov-2018 11:17"), Comment = "feat (milestone summary): add additional columns to income estimate table (incomplete)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74448, CheckInTime = DateTime.Parse("19-Nov-2018 17:06"), Comment = "feat (project viewer): modify income invoice details UI", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74455, CheckInTime = DateTime.Parse("20-Nov-2018 09:22"), Comment = "fix financial summary in external portal to prevent showing Milestone Category column when Milestone Category feature is...", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74506, CheckInTime = DateTime.Parse("21-Nov-2018 11:11"), Comment = "feat (reporting db): add new tables ApplicationBudgetItem and ApplicationBudgetLineItem", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74537, CheckInTime = DateTime.Parse("22-Nov-2018 11:10"), Comment = "fix (approval): prevent Action Grant Offer RAO approval being created when no Grant Schedule document found", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74572, CheckInTime = DateTime.Parse("23-Nov-2018 09:49"), Comment = "fix (Milestone Summary): Income table now displays contributor correctly (income source)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74616, CheckInTime = DateTime.Parse("26-Nov-2018 09:52"), Comment = "Changes to enable Unit Test of Domain classes", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74653, CheckInTime = DateTime.Parse("26-Nov-2018 18:57"), Comment = "fix (Milestone Summary): Remaining column not showing correct amount", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74719, CheckInTime = DateTime.Parse("28-Nov-2018 11:41"), Comment = "fix (Cash Payment) : Undo cash payment creates reversing transaction with with wrong amount", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74767, CheckInTime = DateTime.Parse("29-Nov-2018 13:17"), Comment = "Cannot change the milestone contributor toggle", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74788, CheckInTime = DateTime.Parse("29-Nov-2018 16:26"), Comment = "Branding changes", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74819, CheckInTime = DateTime.Parse("30-Nov-2018 12:53"), Comment = "view to genereate pivot table on Application Budget Items", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 74907, CheckInTime = DateTime.Parse("04-Dec-2018 10:22"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74920, CheckInTime = DateTime.Parse("04-Dec-2018 13:05"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74935, CheckInTime = DateTime.Parse("04-Dec-2018 15:47"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74947, CheckInTime = DateTime.Parse("04-Dec-2018 16:55"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74976, CheckInTime = DateTime.Parse("05-Dec-2018 11:06"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74995, CheckInTime = DateTime.Parse("05-Dec-2018 15:43"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 74996, CheckInTime = DateTime.Parse("05-Dec-2018 15:45"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 75004, CheckInTime = DateTime.Parse("05-Dec-2018 17:47"), Comment = "Branding changes - incomplete", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 75053, CheckInTime = DateTime.Parse("06-Dec-2018 16:29"), Comment = "Feat (external) - add 'Home' next to home icon; change favicon.ico", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 75084, CheckInTime = DateTime.Parse("07-Dec-2018 10:57"), Comment = "fix (styling): update various styles for NHMRC (refer to doc \"Sapphire - Branding phase 2\")", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75093, CheckInTime = DateTime.Parse("07-Dec-2018 12:33"), Comment = "fix (project viewer - external) : Show correct milestones on milestone details view", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75143, CheckInTime = DateTime.Parse("10-Dec-2018 11:49"), Comment = "fix (branding) - Add border to active menu option to separate menu tabs in external application viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "Release 4.8").First().ID },
                new CheckIn { ID = 75144, CheckInTime = DateTime.Parse("10-Dec-2018 11:53"), Comment = "fix (branding) - Add border to active menu option to separate menu tabs in external application viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75152, CheckInTime = DateTime.Parse("10-Dec-2018 13:06"), Comment = "feat (features) : Added new feature OmniNetMileston. Default value false except for NHMRC (true)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75175, CheckInTime = DateTime.Parse("10-Dec-2018 18:14"), Comment = "feat (milestone) : Added fields for Milestone Access", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75285, CheckInTime = DateTime.Parse("13-Dec-2018 12:27"), Comment = "feat (milestone types): domain and model changes to facilitate milestone access", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75331, CheckInTime = DateTime.Parse("14-Dec-2018 11:35"), Comment = "feat (milestone) : further changes to milestone type maintenance, to enforce business rules", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75349, CheckInTime = DateTime.Parse("14-Dec-2018 14:41"), Comment = "feat (position role): new field in PositionRole table; flag ActionMilestone", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75351, CheckInTime = DateTime.Parse("14-Dec-2018 15:14"), Comment = "clean up code", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75376, CheckInTime = DateTime.Parse("17-Dec-2018 11:14"), Comment = "fix (approvals): add scroll bar to list of documents to be generated by bulk letter generation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75413, CheckInTime = DateTime.Parse("17-Dec-2018 18:11"), Comment = "fix (milestones): fix currencyMask on input fields to handle -ve symbol better", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75427, CheckInTime = DateTime.Parse("18-Dec-2018 10:58"), Comment = "fix (UI): Hamburger menu options now highligt when hovered on", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75432, CheckInTime = DateTime.Parse("18-Dec-2018 11:48"), Comment = "fix (milestone summary): Invoiced amount for income milestones was not being shown. Now fixed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75482, CheckInTime = DateTime.Parse("19-Dec-2018 10:27"), Comment = "fix (assessment - external): download assessment summary not finding document", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75547, CheckInTime = DateTime.Parse("20-Dec-2018 12:20"), Comment = "fix (financial summary - external): Invoice Balance and Paid columns now correctly hidden when payments are not allowed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75553, CheckInTime = DateTime.Parse("20-Dec-2018 13:35"), Comment = "fix (financial summary - external): fix so that toggle displays correctly on smaller screens", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID },
                new CheckIn { ID = 75562, CheckInTime = DateTime.Parse("20-Dec-2018 14:57"), Comment = "fix (milestone defaults): fix so that milestone type does not disappear from the drop-down list", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNext").First().ID,
                    TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {TaskID = 118102 } } }
            };

            CheckInBF bf = new CheckInBF(db);
            foreach (CheckIn checkin in checkIns)
            {
                Console.Write(".");
                bf.Create(checkin);
            }

            Console.WriteLine("Done");
            Console.WriteLine();

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsJune(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for June ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                new TaskLog { LogDate = DateTime.Parse("3-June-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("13:45"), Description = "Project variation : REBUS-1072", TaskID = 132512,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79901, CheckInTime = DateTime.Parse("3-June-2019 13:38"), Comment = "feat (project variation): new email template for RIC notification of project variation", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132512 }, new TaskCheckIn { TaskID = 132355 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("3-June-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("3-June-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:55"), Description = "Project variation : REBUS-1072", TaskID = 132512 },
                new TaskLog { LogDate = DateTime.Parse("4-June-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("18:00"), Description = "Project variation : REBUS-1072", TaskID = 132512 },
                new TaskLog { LogDate = DateTime.Parse("5-June-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("14:00"), Description = "Project variation : REBUS-1072", TaskID = 132512,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79984, CheckInTime = DateTime.Parse("5-June-2019 13:34"), Comment = "feat (project variation): further work on workflow and email template for project variation request", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132512 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("5-June-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("14:50"), Description = "Pacman Team Health Check meeting" },
                new TaskLog { LogDate = DateTime.Parse("5-June-2019"), StartTime = TimeSpan.Parse("14:50"), EndTime = TimeSpan.Parse("18:20"), Description = "Project variation : REBUS-1072", TaskID = 132512 },
                new TaskLog { LogDate = DateTime.Parse("6-June-2019"), StartTime = TimeSpan.Parse("08:25"), EndTime = TimeSpan.Parse("12:20"), Description = "Project variation : REBUS-1072", TaskID = 132512,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Standard tokens can be found in file eg GenericFormTokenDefinitionFactory. Can also add client specific tokens by using eg ClientTokenExtensionsForGenericForm. I had to ad this, which also required adding an interface. The interface is the mechanism by which the respective token factory can determine whether there are custom factory's to access" }
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80011, CheckInTime = DateTime.Parse("6-Jun-2019 12:03"), Comment = "feat (project variation): fixed word document template, email template, and added VariationReason custom token", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132512 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("6-June-2019"), StartTime = TimeSpan.Parse("13:20"), EndTime = TimeSpan.Parse("14:00"), Description = "Project variation : REBUS-1072", TaskID = 132512 },
                new TaskLog { LogDate = DateTime.Parse("6-June-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("14:30"), Description = "Annual Review" },
                new TaskLog { LogDate = DateTime.Parse("6-June-2019"), StartTime = TimeSpan.Parse("14:30"), EndTime = TimeSpan.Parse("17:00"), Description = "Project variation : REBUS-1072", TaskID = 132512,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80022, CheckInTime = DateTime.Parse("6-Jun-2019 15:18"), Comment = "feat (project variation): small tweak to email template", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132512 } }
                        },
                        new CheckIn { ID = 80024, CheckInTime = DateTime.Parse("6-Jun-2019 15:47"), Comment = "feat (project variation): small tweak to email template", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132512 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("7-June-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:00"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 133220 },
                new TaskLog { LogDate = DateTime.Parse("7-June-2019"), StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("13:30"), Description = "Company Monthly meeting" },
                new TaskLog { LogDate = DateTime.Parse("7-June-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("17:20"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 133220,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80086, CheckInTime = DateTime.Parse("7-Jun-2019 16:38"), Comment = "remove duplicate item", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 133220 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("11-June-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime=TimeSpan.Parse("12:00"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 133220,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80099, CheckInTime = DateTime.Parse("11-June-2019 11:18"), Comment = "fix (Milestone viewer - external: fix description", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 133220 } }
                        },
                        new CheckIn { ID = 80101, CheckInTime = DateTime.Parse("11-June-2019 11:43"), Comment = "small fix to text", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 133220 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("11-June-2019"), StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("14:00"), Description = "Research Workspace identifier generation: REBUS-1233", TaskID = 132262 },
                new TaskLog { LogDate = DateTime.Parse("11-June-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Sprint Review meeting" },
                new TaskLog { LogDate = DateTime.Parse("11-June-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:00"), Description = "P&C Retrospective meeting" },
                new TaskLog { LogDate = DateTime.Parse("12-June-2019"), StartTime = TimeSpan.Parse("08:30"), EndTime = TimeSpan.Parse("09:30"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 133294,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80121, CheckInTime = DateTime.Parse("12-June-2019 10:29"), Comment = "Small fix to text", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 133294 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("12-June-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Sprint Planning meeting"},
                new TaskLog { LogDate = DateTime.Parse("12-June-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("16:18"), Description = "Research Workspace identifier generation: REBUS-1233", TaskID = 132262,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 80131, CheckInTime = DateTime.Parse("12-June-2019 13:15"), Comment = "fix: project identifier generation now caters for Research Workspaces", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132262 } }
                        },
                        new CheckIn { ID = 80142, CheckInTime = DateTime.Parse("12-June-2019 16:14"), Comment = "migration: new version of workflow", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132262 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("12-June-2019"), StartTime = TimeSpan.Parse("16:18"), EndTime = TimeSpan.Parse("17:30"), Description = "Research Workspace identifier generation: REBUS-1233", TaskID = 133375 }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write("@");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsMay(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for May ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("09:30"), Description = "Must be able to vary deliverable due date to last day of project : REBUS-1054", TaskID = 129495 },
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Sprint Planning meeting" },
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("13:00"), Description = "Must be able to vary deliverable due date to last day of project : REBUS-1054", TaskID = 129495,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78922, CheckInTime = DateTime.Parse("01-May-2019 12:47"), Comment = "fix (milestone variation): change date checks to use .ToUtc() so that due date of milestone can be set equal to end date of projet", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 129495 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("14:00"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133},
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Sprint Retrospective meeting" },
                new TaskLog { LogDate = DateTime.Parse("01-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Need to investigate why save button does not reactive after an error on page"},
                        new TaskLogComment { Comment = "This was my introduction to javaScript promises! When an asynchronous call is made, can use .then() to define a function to run upon successful return. But can also specify a function to run on failure"},
                        new TaskLogComment { Comment = "To fix the problem, I just had to call dirtyFlag reset method in the function that runs on failure. Nice"},
                        new TaskLogComment { Comment = "Actually that was not a sensible fix; better solution was to call the actionmenu's isSaveEnabled property (refer to changeset 78967"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78936, CheckInTime = DateTime.Parse("01-May-2019 16:17"), Comment = "fix (milestone custom properties): ensure that save button reactivates after save of custom properties failed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128133} }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("02-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:15"), Description = "Must be able to vary deliverable due date to last day of project : REBUS-1054", TaskID = 129495,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Needed to fix up the error message when milestone due date set to date outside date range of the project. End date being reported is actually the day before the last day"},
                        new TaskLogComment { Comment = "The start and end dates are saved kind of like UTC eg. 1 Mar 2019 saved as 28 Feb 2019 1PM. Just needed to add .ToLocal() to the dates to get the 'correct' date showing in the error message"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78957, CheckInTime = DateTime.Parse("02-May-2019 11:08"), Comment = "fix (milestone variation): fix error message when due date changed to outside of project dates so that correct start and end dates are displayed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 129495} }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("02-May-2019"), StartTime = TimeSpan.Parse("11:15"), EndTime = TimeSpan.Parse("13:10"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129596,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Original solution of calling dirtyflag.reset was not a good idea. Had the effect of making the duplicated number the non-dirty value. So when user changed the value, the save button enabled, but if they went back to the duplicated number it disabled, making it look that the duplicated value was infact saved to the db"},
                        new TaskLogComment { Comment = "Better solution was to update the isEnabledSave property on the action menu save button. Setting it to true just enables the button, which goes to confirming the the duplicated value has not been saved to the db"},
                        new TaskLogComment { Comment = "The only down-side to this solution is that the original non-dirty value of the property is lost - not a show stopper by any stretch"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78967, CheckInTime = DateTime.Parse("02-May-2019 12:53"), Comment = "fix (milestone custom properties) : fix so that it does not look like a duplicated deliverable number has been saved i.e. save button remains active", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 129596 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("02-May-2019"), StartTime = TimeSpan.Parse("13:10"), EndTime = TimeSpan.Parse("13:25"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129599 },
                new TaskLog { LogDate = DateTime.Parse("02-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129599 },
                new TaskLog { LogDate = DateTime.Parse("03-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("12:30"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129599,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "I didn't actually do a checkin for this - Chris assisted me. Problem was some knockout subscriptions that should have been removed before loading up the new project details. Refer to checkins 78989 and 78990"},
                        new TaskLogComment { Comment = "Previous comment is not accurate. Didn't solve the problem. More to come"}
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("03-May-2019"), StartTime = TimeSpan.Parse("13:20"), EndTime = TimeSpan.Parse("14:20"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129599,
                    Comments = new List<TaskLogComment> { new TaskLogComment { Comment = "Ended up that all I had to do was not subscribe to watching for projectid change when the entity type was a milestone!!"} },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79016, CheckInTime = DateTime.Parse("03-May-2019 14:07"), Comment = "fix (milestone custom properties) : prevent null warnings when changing to different project while on custom properties tab", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 129599 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("03-May-2019"), StartTime = TimeSpan.Parse("14:20"), EndTime = TimeSpan.Parse("15:00"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 129676,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79021, CheckInTime = DateTime.Parse("03-May-2019 14:46"), Comment = "fix (milestone custom properties) : check for duplicate values now excludes deleted milestones", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 129676 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("03-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:00"), Description = "Developer analysis for automatic population of organisations details : REBUS-1058", TaskID = 130018 },
                new TaskLog { LogDate = DateTime.Parse("06-May-2019"), StartTime = TimeSpan.Parse("08:30"), EndTime = TimeSpan.Parse("10:30"), Description = "Developer analysis for automatic population of organisations details : REBUS-1058", TaskID = 130018 },
                new TaskLog { LogDate = DateTime.Parse("06-May-2019"), StartTime = TimeSpan.Parse("10:30"), EndTime = TimeSpan.Parse("12:00"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("06-May-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:00"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("11:10"), Description = "UoM Weekly Review and Planning Session" },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("11:10"), EndTime = TimeSpan.Parse("12:00"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Continuing saga of title -vs- description; back to title, and change detail -> description!!" }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("12:40"), EndTime = TimeSpan.Parse("14:00"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 128133 },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Sprint Health Check meeting" },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:05"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "The side menu tabs are generated in classes. The one for the details tab is 'MilestoneDetailTextTreeViewNode'. It is basically just a constructor that assigns the text, which was hard-coded. Changed it to use OmniResource.Strings.MilestoneDetailLabel.ToString() so that the text can now be configured" },
                        new TaskLogComment { Comment = "Just need to be aware that we did check that details were still saved, but to keep an eye on just incase this change mucks up anything else" }
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79108, CheckInTime = DateTime.Parse("07-May-2019 16:03"), Comment = "fix (milestone viewer) : replace 'Detail' with 'Description' on tab and viewer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128133 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("07-May-2019"), StartTime = TimeSpan.Parse("16:05"), EndTime = TimeSpan.Parse("16:30"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("08-May-2019"), StartTime = TimeSpan.Parse("09:10"), EndTime = TimeSpan.Parse("15:00"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("08-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:05"), Description = "P and C Backlog Refinement meeting"},
                new TaskLog { LogDate = DateTime.Parse("08-May-2019"), StartTime = TimeSpan.Parse("16:05"), EndTime = TimeSpan.Parse("18:45"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114 },
                new TaskLog { LogDate = DateTime.Parse("09-May-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:30"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79183, CheckInTime = DateTime.Parse("09-May-2019 10:32"), Comment = "feat (infiniti datasource) : new datasource to list organisations linked to a person through positions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130114 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("09-May-2019"), StartTime = TimeSpan.Parse("14:15"), EndTime = TimeSpan.Parse("17:30"), Description = "Create datasources for Infiniti : REBUS-1058", TaskID = 130114,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79220, CheckInTime = DateTime.Parse("09-May-2019 17:18"), Comment = "feat (infiniti): fix new datasource to exclude non - current positions", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130114 } }
                        }
                    },
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Needed to sort out why data was not showing up. This was because I had set the json converter to not return a list (line 87 - GetSchema method"}
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("10-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:00"), Description = "Fix links in email templates : REBUS-1121", TaskID = 130621 },
                new TaskLog { LogDate = DateTime.Parse("10-May-2019"), StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("13:30"), Description = "Monthly Company Meeting" },
                new TaskLog { LogDate = DateTime.Parse("10-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Sprint Refinement Meeting" },
                new TaskLog { LogDate = DateTime.Parse("10-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:45"), Description = "Fix links in email templates : REBUS-1121", TaskID = 130621,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79248, CheckInTime = DateTime.Parse("10-May-2019 15:34"), Comment = "fix (templates): fix link to milestone page in external portal in reminder emails", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130621 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("13-May-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("13-May-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("18:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("10:15"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("10:15"), EndTime = TimeSpan.Parse("12:30"), Description = "Fix links in email templates : REBUS-1121", TaskID = 130621,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "There was some differences to wording in templates in SIT which meant that my original migrations didn't update records in SIT"},
                        new TaskLogComment { Comment = "Another interesting thing that I had kind of forgotten, email templates should be created using resx's. By doing this a record is added to TemplateEmailType, which is the proper way for this to work"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn
                        {
                            ID = 79313, CheckInTime = DateTime.Parse("14-May-2019 12:10"), Comment = "fix (templates): fix link to milestone page in external portal in reminder emails", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130621 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("12:30"), EndTime = TimeSpan.Parse("14:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("14:40"), Description = "Sprint Review meeting" },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("14:40"), EndTime = TimeSpan.Parse("15:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:00"), Description = "Sprint Retrospective meeting" },
                new TaskLog { LogDate = DateTime.Parse("14-May-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("15-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("10:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("15-May-2019"), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("10:45"), Description = "Sprint Planning meeting" },
                new TaskLog { LogDate = DateTime.Parse("15-May-2019"), StartTime = TimeSpan.Parse("10:45"), EndTime = TimeSpan.Parse("17:15"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("16-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("17:15"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771 },
                new TaskLog { LogDate = DateTime.Parse("17-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:30"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 130771,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79421, CheckInTime = DateTime.Parse("17-May-2019 11:06"), Comment = "feat (application viewer - external): add new page to view documents of type Contract", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130771 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("17-May-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("15:00"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("17-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("15:30"), Description = "Sprint Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("17-May-2019"), StartTime = TimeSpan.Parse("15:30"), EndTime = TimeSpan.Parse("16:15"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("20-May-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("11:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131860 },
                new TaskLog { LogDate = DateTime.Parse("20-May-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("17:10"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("21-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131860 },
                new TaskLog { LogDate = DateTime.Parse("21-May-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("11:40"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131862,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79517, CheckInTime = DateTime.Parse("21-May-2019 11:37"), Comment = "fix (application viewer - external): fix check boxes on contract viewer so that they can be selected", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 131862 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("21-May-2019"), StartTime = TimeSpan.Parse("11:40"), EndTime = TimeSpan.Parse("12:10"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131865,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79521, CheckInTime = DateTime.Parse("21-May-2019 12:08"), Comment = "fix (application viewer - external): correct captions on page", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 131865 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("21-May-2019"), StartTime = TimeSpan.Parse("12:10"), EndTime = TimeSpan.Parse("15:45"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131860,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79522, CheckInTime = DateTime.Parse("21-May-2019 13:46"), Comment = "fix (application viewer - external): add ellipses for edit contract", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 131860 } }
                        },
                        new CheckIn { ID = 79530, CheckInTime = DateTime.Parse("21-May-2019 15:39"), Comment = "fix (application viewer - external): add 'edit contract option' button [not implemented yet]", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 131860 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("21-May-2019"), StartTime = TimeSpan.Parse("15:45"), EndTime = TimeSpan.Parse("17:10"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("22-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("10:15"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("22-May-2019"), StartTime = TimeSpan.Parse("10:15"), EndTime = TimeSpan.Parse("11:40"), Description = "Sprint Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("22-May-2019"), StartTime = TimeSpan.Parse("11:40"), EndTime = TimeSpan.Parse("17:10"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719 },
                new TaskLog { LogDate = DateTime.Parse("23-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("16:00"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79623, CheckInTime = DateTime.Parse("23-May-2019 13:02"), Comment = "feat (application viewer - external): add ServiceNow tab to viewer [first version, dummy data, no table for data yet]", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130719 } }
                        },
                        new CheckIn { ID = 79628, CheckInTime = DateTime.Parse("23-May-2019 14:02"), Comment = "feat (application viewer - external): small edit", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130719 } }
                        },
                        new CheckIn { ID = 79638, CheckInTime = DateTime.Parse("23-May-2019 15:15"), Comment = "feat (application viewer - external): add two fields to table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130719 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("23-May-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("16:50"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 132252 },
                new TaskLog { LogDate = DateTime.Parse("24-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("10:00"), Description = "Must be able to vary deliverable due date to last day of project : REBUS-1054", TaskID = 129495,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Product team made some changes to MilestoneService which caused date to be saved incorrectly (i.e sometimes the day prior to the date entered. Is some sort of UTC problem, and product team are fixing. Took the time because I wanted to get code as per 1 May 2019 to make sure that I had it right then, which I did" }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("24-May-2019"), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("16:50"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 132252 },
                new TaskLog { LogDate = DateTime.Parse("27-May-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("11:25"), Description = "Fix Project Viewer : REBUS-1226", TaskID = 132252,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79701, CheckInTime = DateTime.Parse("27-May-2019 11:22"), Comment = "fix (Project Viewer - external): side menu header was incorrect when viewing milestone details", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132252 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("27-May-2019"), StartTime = TimeSpan.Parse("11:25"), EndTime = TimeSpan.Parse("11:40"), Description = "ServiceNow viewer in application viewer : REBUS-949", TaskID = 130719,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79702, CheckInTime = DateTime.Parse("27-May-2019 11:29"), Comment = "feat (application viewer - external): small edit", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 130719 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("27-May-2019"), StartTime = TimeSpan.Parse("11:40"), EndTime = TimeSpan.Parse("13:00"), Description = "Add a summary of contracts into the application viewer : REBUS-1063", TaskID = 131862,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Fix to enable individual check-boxes to be selected on document list tab" }
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79703, CheckInTime = DateTime.Parse("27-May-2019 11:57"), Comment = "fix (application viewer - external): enable user to be able to select and de-select individual documents on the documents tab", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 131862 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("27-May-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Change name of Application download document : REBUS-1239", TaskID = 132284 },
                new TaskLog { LogDate = DateTime.Parse("27-May-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Edit application status' : REBUS-1256", TaskID = 132285,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79720, CheckInTime = DateTime.Parse("27-May-2019 16:39"), Comment = "migration: Successful and Unsuccessful Application Status is now visible in external portal", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132285 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("28-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("14:00"), Description = "New form for project variation : REBUS-567", TaskID = 132355 },
                new TaskLog { LogDate = DateTime.Parse("28-May-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:00"), Description = "Sprint Retrospective Meeting" },
                new TaskLog { LogDate = DateTime.Parse("28-May-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("17:15"), Description = "P & C Retrospective meeting" },
                new TaskLog { LogDate = DateTime.Parse("29-May-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("09:30"), Description = "New form for project variation : REBUS-567", TaskID = 132355 },
                new TaskLog { LogDate = DateTime.Parse("29-May-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Sprint Planning meeting" },
                new TaskLog { LogDate = DateTime.Parse("29-May-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("14:25"), Description = "New form for project variation : REBUS-567", TaskID = 132356,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79779, CheckInTime = DateTime.Parse("29-May-2019 14:20"), Comment = "feat (project variation): migration to add form mapping for 'Project Variation' action", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 132356 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("29-May-2019"), StartTime = TimeSpan.Parse("14:25"), EndTime = TimeSpan.Parse("16:30"), Description = "New form for project variation : REBUS-567", TaskID = 132355,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79783, CheckInTime = DateTime.Parse("29-May-2019 15:07"), Comment = "Added new form 'UOM Project Variation'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132355 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("30-May-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("17:00"), Description = "New form for project variation : REBUS-567", TaskID = 132355,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 79830, CheckInTime = DateTime.Parse("30-May-2019 16:01"), Comment = "migration to create workflow [stub] for project variation form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 132357 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("31-May-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:30"), Description = "New form for project variation : REBUS-567", TaskID = 132357 },
                new TaskLog { LogDate = DateTime.Parse("31-May-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("18:00"), Description = "New form for project variation : REBUS-567", TaskID = 132357 }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write("$");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsApr(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for April ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                new TaskLog { LogDate = DateTime.Parse("01-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:17"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("01-Apr-2019"), StartTime = TimeSpan.Parse("12:17"), EndTime = TimeSpan.Parse("13:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("01-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("15:50"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78160, CheckInTime = DateTime.Parse("01-Apr-2019 14:44"), Comment = "fix (proposal budget) - reinstate non-salary sections that somehow got removed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        },
                        new CheckIn {  ID = 78162, CheckInTime = DateTime.Parse("01-Apr-2019 15:14"), Comment = "fix (proposal budget) - reinstate non-salary sections that somehow got removed", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("01-Apr-2019"), StartTime = TimeSpan.Parse("15:50"), EndTime = TimeSpan.Parse("16:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("01-Apr-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Researcher Journey Walkthrough" },
                new TaskLog { LogDate = DateTime.Parse("02-Apr-2019"), StartTime = TimeSpan.Parse("09:15"), EndTime = TimeSpan.Parse("11:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("02-Apr-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("02-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("03-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78216, CheckInTime = DateTime.Parse("03-Apr-2019 10:44"), Comment = "fix (Form): move research workspace lookup to top of page in Full Application", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("03-Apr-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("14:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("03-Apr-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("03-Apr-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:55"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78245, CheckInTime = DateTime.Parse("03-Apr-2019 16:51"), Comment = "feat (external): add site menu tab for projects (Deliverable Projects)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } }
                        }
                     } },
                new TaskLog { LogDate = DateTime.Parse("03-Apr-2019"), StartTime = TimeSpan.Parse("16:55"), EndTime = TimeSpan.Parse("17:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78246, CheckInTime = DateTime.Parse("03-Apr-2019 16:57"), Comment = "fix (infiniti): add property that combines project Identifier and Title for use in research workspace lookups", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("04-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("09:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("04-Apr-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("10:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("04-Apr-2019"), StartTime = TimeSpan.Parse("10:30"), EndTime = TimeSpan.Parse("10:50"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78257, CheckInTime = DateTime.Parse("04-Apr-2019 10:49"), Comment = "fix (infiniti): change name of property (IdentifierAndTitle) and alter format of lookup text", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("04-Apr-2019"), StartTime = TimeSpan.Parse("10:50"), EndTime = TimeSpan.Parse("13:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("04-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:15"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78272, CheckInTime = DateTime.Parse("04-Apr-2019 16:23"), Comment = "feat (external): add new page to display deliverable projects", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("05-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("05-Apr-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("13:30"), Description = "Company meetings"},
                new TaskLog { LogDate = DateTime.Parse("05-Apr-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("05-Apr-2019"), StartTime = TimeSpan.Parse("15:00"), EndTime = TimeSpan.Parse("16:00"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("05-Apr-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("17:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },

                new TaskLog { LogDate = DateTime.Parse("08-Apr-2019"), StartTime = TimeSpan.Parse("09:15"), EndTime = TimeSpan.Parse("12:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("08-Apr-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78354, CheckInTime = DateTime.Parse("08-Apr-2019 16:34"), Comment = "fix (migrations): add migrations to fix classification settings", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("09:15"), EndTime = TimeSpan.Parse("10:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("10:30"), EndTime = TimeSpan.Parse("11:00"), Description = "Contracts and ServiceNow User Stories Meeting" },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("13:30"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("14:50"), Description = "Sprint Review Meeting" },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("14:50"), EndTime = TimeSpan.Parse("16:00"), Description = "Sprint Retrospective Meeting" },
                new TaskLog { LogDate = DateTime.Parse("09-Apr-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78402, CheckInTime = DateTime.Parse("09-Apr-2019 16:27"), Comment = "fix (project view): fix so dates sort correctly", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } }
                        },
                        new CheckIn {  ID = 78404, CheckInTime = DateTime.Parse("09-Apr-2019 16:38"), Comment = "fix bad check-in", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("10-Apr-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("09:30"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("10-Apr-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("10:10"), Description = "Sprint Planning meeting" },
                new TaskLog { LogDate = DateTime.Parse("10-Apr-2019"), StartTime = TimeSpan.Parse("10:10"), EndTime = TimeSpan.Parse("13:00"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("10-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("19:00"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78462, CheckInTime = DateTime.Parse("10-Apr-2019 18:55"), Comment = "migration (uom): migration to add new milestone types and custom properties", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126760 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("10:30"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("10:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Technical Review meeting" },
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("12:30"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("14:30"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("14:30"), EndTime = TimeSpan.Parse("15:30"), Description = "Technical Review meeting" },
                new TaskLog { LogDate = DateTime.Parse("11-Apr-2019"), StartTime = TimeSpan.Parse("15:30"), EndTime = TimeSpan.Parse("19:00"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("12-Apr-2019"), StartTime = TimeSpan.Parse("08:15"), EndTime = TimeSpan.Parse("13:00"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78517, CheckInTime = DateTime.Parse("12-Apr-2019 10:48"), Comment = "migration (uom): migration to add new milestone types and custom properties", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } }
                        },
                        new CheckIn { ID = 78518, CheckInTime = DateTime.Parse("12-Apr-2019 11:06"), Comment = "fix up checkin of Newtonsoft.Json", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } }
                        },
                        new CheckIn { ID = 78521, CheckInTime = DateTime.Parse("12-Apr-2019 11:50"), Comment = "migration (uom): add missing strings", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("12-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("16:30"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962 },
                new TaskLog { LogDate = DateTime.Parse("15-Apr-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760},
                new TaskLog { LogDate = DateTime.Parse("15-Apr-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:45"), Description = "Create Migrations for Milestone Types : REBUS-938", TaskID = 126760,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78575, CheckInTime = DateTime.Parse("15-Apr-2019 17:07"), Comment = "migrations : fix string resources so that Milestone is tokenized", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126760 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("16-Apr-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("11:00"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962 },
                new TaskLog { LogDate = DateTime.Parse("16-Apr-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("12:00"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("16-Apr-2019"), StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("13:15"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78601, CheckInTime = DateTime.Parse("16-Apr-2019 13:01"), Comment = "migrations : fix string resources so that Milestone is tokenized", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("16-Apr-2019"), StartTime = TimeSpan.Parse("13:45"), EndTime = TimeSpan.Parse("14:45"), Description = "Initial Developer Analysis : REBUS-1014", TaskID = 127260 },
                new TaskLog { LogDate = DateTime.Parse("16-Apr-2019"), StartTime = TimeSpan.Parse("14:45"), EndTime = TimeSpan.Parse("17:55"), Description = "Develop migration to set up required data : REBUS-1014", TaskID = 127299 },
                new TaskLog { LogDate = DateTime.Parse("17-Apr-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("10:20"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78629, CheckInTime = DateTime.Parse("17-Apr-2019 10:15"), Comment = "string resource: tokenize items in drop-down for determining which milestones to display", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("17-Apr-2019"), StartTime = TimeSpan.Parse("10:20"), EndTime = TimeSpan.Parse("13:55"), Description = "Develop migration to set up required data : REBUS-1014", TaskID = 127299 },
                new TaskLog { LogDate = DateTime.Parse("17-Apr-2019"), StartTime = TimeSpan.Parse("14:28"), EndTime = TimeSpan.Parse("19:15"), Description = "Develop migration to set up required data : REBUS-1014", TaskID = 127299,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78658, CheckInTime = DateTime.Parse("17-Apr-2019 18:06"), Comment = "fix (generic lookups): correct processing of float numerics", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 127299 } } },
                        new CheckIn { ID = 78659, CheckInTime = DateTime.Parse("17-Apr-2019 19:06"), Comment = "migrations (UoM): new classifications for Default Currency List and Cost Recovery List", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 127299 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("18-Apr-2019"), StartTime = TimeSpan.Parse("08:35"), EndTime = TimeSpan.Parse("12:25"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78673, CheckInTime = DateTime.Parse("18-Apr-2019 10:20"), Comment = "fix (create milestone): asterisk for required fields now displays against Title and Milestone Type labels", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("18-Apr-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("16:30"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78698, CheckInTime = DateTime.Parse("18-Apr-2019 15:56"), Comment = "fix (milestone custom properties): asterisk for required fields now displays against Title and Milestone Type labels", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("23-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:45"), Description = "Fix Milestone Variance Wizard to prevent exception : REBUS-1031", TaskID = 127774 },
                new TaskLog { LogDate = DateTime.Parse("23-Apr-2019"), StartTime = TimeSpan.Parse("11:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Create Migrations for Milestone Types : REBUS-874", TaskID = 126962,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78732, CheckInTime = DateTime.Parse("23-Apr-2019 12:38"), Comment = "fix (custom properties): change heading on Custom Property pages", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 126962 } } }
                        } },
                new TaskLog { LogDate = DateTime.Parse("23-Apr-2019"), StartTime = TimeSpan.Parse("12:45"), EndTime = TimeSpan.Parse("13:30"), Description = "Fix Milestone Variance Wizard to prevent exception : REBUS-1031", TaskID = 127774 },
                new TaskLog { LogDate = DateTime.Parse("23-Apr-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:20"), Description = "Fix Milestone Variance Wizard to prevent exception : REBUS-1031", TaskID = 127774,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78745, CheckInTime = DateTime.Parse("23-Apr-2019 15:14"), Comment = "fix (milestone variance): fixed the setting of CreditorCurrentDueDate which was causing exception", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 127774 } } },
                        new CheckIn { ID = 78748, CheckInTime = DateTime.Parse("23-Apr-2019 15:44"), Comment = "fix (data migration): added migration to update null values of CreditorCurrentDueDate field on Milestone records", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 127774 } } }
                        } },
                new TaskLog { LogDate = DateTime.Parse("23-Apr-2019"), StartTime = TimeSpan.Parse("16:20"), EndTime = TimeSpan.Parse("17:15"), Description = "Fix saving Milestone Custom Properties : REBUS-1030", TaskID = 128012,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Need to work out why changes in Delivery Number do not trigger isDirty"}
                        } },
                new TaskLog { LogDate = DateTime.Parse("24-Apr-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("12:00"), Description = "Fix saving Milestone Custom Properties : REBUS - 1030", TaskID = 128012},
                new TaskLog { LogDate = DateTime.Parse("24-Apr-2019"), StartTime = TimeSpan.Parse("12:30"), EndTime = TimeSpan.Parse("13:45"), Description = "Fix saving Milestone Custom Properties : REBUS - 1030", TaskID = 128012,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "The problem was the boolean custom property. When custom properties have not been saved, then on load the value is set to null. Just entering a Deliverable Number triggered the dirty flag, but when saving validation failed because the boolean field had not been set. Fixed by forcing default value of false on any boolean custom properties",}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78770, CheckInTime = DateTime.Parse("24-Apr-2019 13:41"), Comment = "fix (custom properties): ensure custom properties are saved properly when boolean custom properties are present", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128012 } } }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("24-Apr-2019"), StartTime = TimeSpan.Parse("13:45"), EndTime = TimeSpan.Parse("17:15"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "First try is to create a function that [on the server] checks if value already exists for a milestone on the project" }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("26-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:15"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Adding new field 'IsUnique' to CustomPropertyDefinition"},
                        new TaskLogComment { Comment = "The following models had IsUnique field added: CustomPropertyDefinition(2), CustomPropertyDefinitionModel, CustomPropertyModel, CustomProperty, CustomPropertyPortfolioModel, ContactCustomPropertyModel"},
                        new TaskLogComment { Comment = "Custom Properties can be applied to: Application, DecisionScenario, Meeting, Milestone, Organisation, Panel, Person, Portfolio, Position, ProjectApplication, ProjectProgram, RoundStage, System, Unit"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78800, CheckInTime = DateTime.Parse("26-Apr-2019 10:17"), Comment = "migrations: add 'IsUnique' field to CustomPropertyDefinition table", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128133 } }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("26-Apr-2019"), StartTime = TimeSpan.Parse("11:15"), EndTime = TimeSpan.Parse("11:35"), Description = "Developer Meeting"},
                new TaskLog { LogDate = DateTime.Parse("26-Apr-2019"), StartTime = TimeSpan.Parse("11:35"), EndTime = TimeSpan.Parse("13:15"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Changes to UI to enable updating the value of IsUnique"}
                    },
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 78810, CheckInTime = DateTime.Parse("26-Apr-2019 12:23"), Comment = "feat (custom properties): added IsUnique field to custom properties UI", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128133} }
                        }
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("26-Apr-2019"), StartTime = TimeSpan.Parse("13:45"), EndTime = TimeSpan.Parse("17:05"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Add validation check in CustomPropertyService for custom properties that must be unique"}
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("29-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("17:45"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    Comments = new List<TaskLogComment>
                    {
                        new TaskLogComment { Comment = "Need to add new repository for CustomPropertyMilestones. Need to update BusinessSeviceNinjectModule. Also add mapper and mapper interface. And update OmniBaseDomainServiceModule"}
                    }
                },
                new TaskLog { LogDate = DateTime.Parse("30-Apr-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:30"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133 },
                new TaskLog { LogDate = DateTime.Parse("30-Apr-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:05"), Description = "Sprint Review Meeting" },
                new TaskLog { LogDate = DateTime.Parse("30-Apr-2019"), StartTime = TimeSpan.Parse("15:05"), EndTime = TimeSpan.Parse("19:00"), Description = "Prevent duplicate Deliverable Number on milestones within project : REBUS - 1032", TaskID = 128133,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID =78906, CheckInTime = DateTime.Parse("30-Apr-2019 18:49"), Comment = "feat (custom properties - milestones) : add check for unique values within all milestones in the project", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 128133 } }
                        }
                    }
                }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write("&");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsMar(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for March ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                new TaskLog { LogDate = DateTime.Parse("01-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:25"), Description = "Build REBUSS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("01-Mar-2019"), StartTime = TimeSpan.Parse("14:10"), EndTime = TimeSpan.Parse("18:50"), Description = "Build REBUSS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77313, CheckInTime = DateTime.Parse("01-Mar-2019 19:09"), Comment = "feat (external forms): part solution to linking items to containers, specifically Idea form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("04-Mar-2019"), StartTime = TimeSpan.Parse("10:00"), EndTime = TimeSpan.Parse("12:55"), Description = "Build REBUSS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("04-Mar-2019"), StartTime = TimeSpan.Parse("13:35"), EndTime = TimeSpan.Parse("16:30"), Description = "Build REBUSS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("05-Mar-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("13:02"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77362, CheckInTime = DateTime.Parse("05-Mar-2019 09:39"), Comment = "fix to prevent config warning", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn {  TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("05-Mar-2019"), StartTime = TimeSpan.Parse("13:32"), EndTime = TimeSpan.Parse("17:55"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77392, CheckInTime = DateTime.Parse("05-Mar-2019 17:31"), Comment = "feat (external forms): part solution to linking items to containers, specifically IP Disclosure form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } },
                        new CheckIn {  ID = 77393, CheckInTime = DateTime.Parse("05-Mar-2019 17:38"), Comment = "feat (external forms): part solution to linking items to containers, specifically IP Disclosure form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("06-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("06-Mar-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("17:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("07-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("07-Mar-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("17:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("08-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77538, CheckInTime = DateTime.Parse("08-Mar-2019 11:57"), Comment = "add id's to RemovedMigrations to prevent configuration warning", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("08-Mar-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("17:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                         new CheckIn {  ID = 77545, CheckInTime = DateTime.Parse("08-Mar-2019 14:26"), Comment = "Quick fix ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } },
                         new CheckIn {  ID = 77553, CheckInTime = DateTime.Parse("08-Mar-2019 15:47"), Comment = "Update UoM Idea form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("12-Mar-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("12:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77595, CheckInTime = DateTime.Parse("12-Mar-2019 10:54"), Comment = "fix label on IP and Compliance section on Project Viewer (external portal)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("12-Mar-2019"), StartTime = TimeSpan.Parse("12:45"), EndTime = TimeSpan.Parse("16:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77601, CheckInTime = DateTime.Parse("12-Mar-2019 12:52"), Comment = "New version of UOM Proposal Budget - link to Research Workspace", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("12-Mar-2019"), StartTime = TimeSpan.Parse("16:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Sprint Review" },
                new TaskLog { LogDate = DateTime.Parse("12-Mar-2019"), StartTime = TimeSpan.Parse("17:00"), EndTime = TimeSpan.Parse("18:15"), Description = "Sprint Retrospective" },
                new TaskLog { LogDate = DateTime.Parse("13-Mar-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("11:00"), Description = "Sprint Planning" },
                new TaskLog { LogDate = DateTime.Parse("13-Mar-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("13-Mar-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:40"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("13-Mar-2019"), StartTime = TimeSpan.Parse("17:40"), EndTime = TimeSpan.Parse("18:10"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77626, CheckInTime = DateTime.Parse("13-Mar-2019 13:21"), Comment = "Enable submit button on UOM Idea and UOM IP Disclosure forms only when linked to research project", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },

                new TaskLog { LogDate = DateTime.Parse("14-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77656, CheckInTime = DateTime.Parse("14-Mar-2019 10:52"), Comment = "feat (workflow): add workflow to advance proprosal from status 'Waiting Approval: Execcute Contract '", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("14-Mar-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("15:55"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77671, CheckInTime = DateTime.Parse("14-Mar-2019 15:51"), Comment = "New version of UOM Proposal Application - link to Research Workspace", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("14-Mar-2019"), StartTime = TimeSpan.Parse("15:55"), EndTime = TimeSpan.Parse("17:00"), Description = "Sprint Health Check Meeting" },
                new TaskLog { LogDate = DateTime.Parse("15-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:00"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("15-Mar-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("12:00"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("15-Mar-2019"), StartTime = TimeSpan.Parse("12:30"), EndTime = TimeSpan.Parse("17:15"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("18-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:10"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("18-Mar-2019"), StartTime = TimeSpan.Parse("12:40"), EndTime = TimeSpan.Parse("16:40"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77766, CheckInTime = DateTime.Parse("18-Mar-2019 12:53"), Comment = "fix form and workflow to cope with \"WF - appended to round name\"", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("19-Mar-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("09:50"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77805, CheckInTime = DateTime.Parse("19-Mar-2019 09:57"), Comment = "Spelling fixes", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("19-Mar-2019"), StartTime = TimeSpan.Parse("09:50"), EndTime = TimeSpan.Parse("12:50"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77825, CheckInTime = DateTime.Parse("19-Mar-2019 14:45"), Comment = "Fix application form so that proposal shows up in research workspace", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("19-Mar-2019"), StartTime = TimeSpan.Parse("13:20"), EndTime = TimeSpan.Parse("16:10"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77839, CheckInTime = DateTime.Parse("19-Mar-2019 18:40"), Comment = "Form updates for Idea, IP Disclosure, Proposal and Proposal Budget", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("19-Mar-2019"), StartTime = TimeSpan.Parse("16:10"), EndTime = TimeSpan.Parse("19:00"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77840, CheckInTime = DateTime.Parse("19-Mar-2019 19:06"), Comment = "Form updates for Idea, IP Disclosure, Proposal and Proposal Budget", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124839 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("20-Mar-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("10:42"), Description = "Workflow for Approve / Reject Execute Contract : REBUS-427", TaskID = 124839 },
                new TaskLog { LogDate = DateTime.Parse("20-Mar-2019"), StartTime = TimeSpan.Parse("10:42"), EndTime = TimeSpan.Parse("11:54"), Description = "Workflow for Execute Contract : REBUS-419", TaskID = 125282 },
                new TaskLog { LogDate = DateTime.Parse("20-Mar-2019"), StartTime = TimeSpan.Parse("12:24"), EndTime = TimeSpan.Parse("14:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("20-Mar-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:05"), Description = "Backlog Refinement Meeting" },
                new TaskLog { LogDate = DateTime.Parse("20-Mar-2019"), StartTime = TimeSpan.Parse("15:05"), EndTime = TimeSpan.Parse("19:00"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77872, CheckInTime = DateTime.Parse("20-Mar-2019 18:01"), Comment = "Add new project statuses, update application status for transfer application processing", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("21-Mar-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("12:05"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("21-Mar-2019"), StartTime = TimeSpan.Parse("12:35"), EndTime = TimeSpan.Parse("16:45"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290 },
                new TaskLog { LogDate = DateTime.Parse("22-Mar-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("12:40"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77934, CheckInTime = DateTime.Parse("22-Mar-2019 10:57"), Comment = "feat (project): migration to set default project status to 'Setup'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("22-Mar-2019"), StartTime = TimeSpan.Parse("13:10"), EndTime = TimeSpan.Parse("16:50"), Description = "Project Establishment Part 1 : Setup : REBUS-199", TaskID = 125290,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77951, CheckInTime = DateTime.Parse("22-Mar-2019 16:25"), Comment = "feat (project-external): only research workspaces are listed on Research Workspace tab (not Projects)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 125290 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("25-Mar-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("13:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("25-Mar-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:05"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("25-Mar-2019"), StartTime = TimeSpan.Parse("15:05"), EndTime = TimeSpan.Parse("17:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77996, CheckInTime = DateTime.Parse("25-Mar-2019 17:52"), Comment = "fix (infiniti): enable form to be unlinked from Research Workspace", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } } }
                    } },

                new TaskLog { LogDate = DateTime.Parse("26-Mar-2019"), StartTime = TimeSpan.Parse("08:50"), EndTime = TimeSpan.Parse("12:32"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("26-Mar-2019"), StartTime = TimeSpan.Parse("13:02"), EndTime = TimeSpan.Parse("14:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("26-Mar-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("16:30"), Description = "Sprint Review and Sprint Retrospective meetings" },
                new TaskLog { LogDate = DateTime.Parse("26-Mar-2019"), StartTime = TimeSpan.Parse("16:30"), EndTime = TimeSpan.Parse("18:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("27-Mar-2019"), StartTime = TimeSpan.Parse("09:15"), EndTime = TimeSpan.Parse("09:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("27-Mar-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Sprint Planning meeting"  },
                new TaskLog { LogDate = DateTime.Parse("27-Mar-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("12:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390},
                new TaskLog { LogDate = DateTime.Parse("27-Mar-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("18:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78075, CheckInTime = DateTime.Parse("27-Mar-2019 17:09"), Comment = "fix (forms): add filter on research workspace lookup to restrict to research workspace projects only (CT)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                    } } },
                new TaskLog { LogDate = DateTime.Parse("28-Mar-2019"), StartTime = TimeSpan.Parse("08:10"), EndTime = TimeSpan.Parse("10:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78086, CheckInTime = DateTime.Parse("28-Mar-2019 09:34"), Comment = "Fix to Proposal Stub form to allow de-linking to Research Workspace", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("28-Mar-2019"), StartTime = TimeSpan.Parse("10:40"), EndTime = TimeSpan.Parse("12:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 },
                new TaskLog { LogDate = DateTime.Parse("28-Mar-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("17:45"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78114, CheckInTime = DateTime.Parse("28-Mar-2019 16:18"), Comment = "fix (forms): show workspace identifier against selected Research workspace on Idea and budget forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("29-Mar-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:30"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78122, CheckInTime = DateTime.Parse("29-Mar-2019 10:21"), Comment = "fix (forms): show workspace identifier against selected Research workspace on proposal and proposal stub forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } },
                new TaskLog { LogDate = DateTime.Parse("29-Mar-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 78132, CheckInTime = DateTime.Parse("29-Mar-2019 14:15"), Comment = "fix : migration to add back Program CT", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        },
                        new CheckIn {  ID = 78138, CheckInTime = DateTime.Parse("29-Mar-2019 16:45"), Comment = "'fix workflow 'Project Registration'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124390 } }
                        }
                    } }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write("*");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsFeb(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for February ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                new TaskLog { LogDate = DateTime.Parse("01-Feb-2019"), StartTime = TimeSpan.Parse("08:43"), EndTime = TimeSpan.Parse("12:43"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("01-Feb-2019"), StartTime = TimeSpan.Parse("13:13"), EndTime = TimeSpan.Parse("16:44"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("04-Feb-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("04-Feb-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("17:15"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("05-Feb-2019"), StartTime = TimeSpan.Parse("08:44"), EndTime = TimeSpan.Parse("13:31"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("05-Feb-2019"), StartTime = TimeSpan.Parse("14:01"), EndTime = TimeSpan.Parse("18:01"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("06-Feb-2019"), StartTime = TimeSpan.Parse("08:44"), EndTime = TimeSpan.Parse("13:43"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("06-Feb-2019"), StartTime = TimeSpan.Parse("14:14"), EndTime = TimeSpan.Parse("18:14"), Description = "Develop unit tests for create new milestone", TaskID = 119328,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76492, CheckInTime = DateTime.Parse("06-Feb-2019 18:33"), Comment = "Add unit test for Create new milestone", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 119328 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("07-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:29"), Description = "Updates to create milestone page", TaskID = 116714 },
                new TaskLog { LogDate = DateTime.Parse("07-Feb-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Updates to create milestone page", TaskID = 116714,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76542, CheckInTime = DateTime.Parse("07-Feb-2019 17:03"), Comment = "feat (milestone): fix display of budget category breakdown", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116714 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("08-Feb-2019"), StartTime = TimeSpan.Parse("08:55"), EndTime = TimeSpan.Parse("12:15"), Description = "Change to original milestone page to show budget category breakdowns", TaskID = 116729 },
                new TaskLog { LogDate = DateTime.Parse("08-Feb-2019"), StartTime = TimeSpan.Parse("12:45"), EndTime = TimeSpan.Parse("16:45"), Description = "Change to original milestone page to show budget category breakdowns", TaskID = 116729 },
                new TaskLog { LogDate = DateTime.Parse("11-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Agile Training at University of Melbourne" },
                new TaskLog { LogDate = DateTime.Parse("11-Feb-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:30"), Description = "Agile Training at University of Melbourne" },
                new TaskLog { LogDate = DateTime.Parse("12-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Agile Training at University of Melbourne" },
                new TaskLog { LogDate = DateTime.Parse("12-Feb-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:30"), Description = "Agile Training at University of Melbourne" },
                new TaskLog { LogDate = DateTime.Parse("13-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("13:00"), Description = "Initial sprint planning and orientation" },
                new TaskLog { LogDate = DateTime.Parse("13-Feb-2019"), StartTime = TimeSpan.Parse("13:30"), EndTime = TimeSpan.Parse("17:30"), Description = "Initial sprint planning and orientation" },
                new TaskLog { LogDate = DateTime.Parse("14-Feb-2019"), StartTime = TimeSpan.Parse("09:05"), EndTime = TimeSpan.Parse("12:50"), Description = "Setting up environment on laptop" },
                new TaskLog { LogDate = DateTime.Parse("14-Feb-2019"), StartTime = TimeSpan.Parse("13:20"), EndTime = TimeSpan.Parse("17:20"), Description = "Setting up environment on laptop" },
                new TaskLog { LogDate = DateTime.Parse("15-Feb-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:30"), Description = "Data migrations", TaskID = 122532 },
                new TaskLog { LogDate = DateTime.Parse("15-Feb-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("16:00"), Description = "Data migrations", TaskID = 122532,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 76796, CheckInTime = DateTime.Parse("15-Feb-2019 15:48"), Comment = "Config: add classification records for 'Research type', Funder' and 'Multi institution agreement'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 122532 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("18-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("17:00"), Description = "Annual leave" },
                new TaskLog { LogDate = DateTime.Parse("19-Feb-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("10:40"), Description = "Data migrations", TaskID = 122925 },
                new TaskLog { LogDate = DateTime.Parse("19-Feb-2019"), StartTime = TimeSpan.Parse("10:40"), EndTime = TimeSpan.Parse("12:10"), Description = "Data migrations", TaskID = 122926,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76893, CheckInTime = DateTime.Parse("19-Feb-2019 11:56"), Comment = "Adding configurations for UoM", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 122925 }, new TaskCheckIn { TaskID = 122926 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("19-Feb-2019"), StartTime = TimeSpan.Parse("12:40"), EndTime = TimeSpan.Parse("17:00"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("20-Feb-2019"), StartTime = TimeSpan.Parse("09:05"), EndTime = TimeSpan.Parse("13:29"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("20-Feb-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("15:05"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("20-Feb-2019"), StartTime = TimeSpan.Parse("15:05"), EndTime = TimeSpan.Parse("17:40"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76955, CheckInTime = DateTime.Parse("20-Feb-2019 14:04"), Comment = "fix (workflows): fix to Program of Work Registration", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 122971 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("20-Feb-2019"), StartTime = TimeSpan.Parse("17:40"), EndTime = TimeSpan.Parse("18:20"), Description = "Data migrations : REBUS-188", TaskID = 123653 },
                new TaskLog { LogDate = DateTime.Parse("21-Feb-2019"), StartTime = TimeSpan.Parse("08:42"), EndTime = TimeSpan.Parse("11:11"), Description = "Data migrations : REBUS-188", TaskID = 123653,
                    CheckIns = new List<CheckIn>
                    {
                     new CheckIn {  ID = 76991, CheckInTime = DateTime.Parse("21-Feb-2019 11:09"), Comment = "fix (migrations): remove 'RO *' rounds and roundstages", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                        TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 123653 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("21-Feb-2019"), StartTime = TimeSpan.Parse("11:11"), EndTime = TimeSpan.Parse("12:36"), Description = "Data migrations : REBUS-188", TaskID = 123667,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76999, CheckInTime = DateTime.Parse("21-Feb-2019 12:35"), Comment = "feat (migrations): Add migration for 'Idea' round and roundstage", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 123667 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("21-Feb-2019"), StartTime = TimeSpan.Parse("13:07"), EndTime = TimeSpan.Parse("17:59"), Description = "Data migrations : REBUS-188", TaskID = 123667,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77027, CheckInTime = DateTime.Parse("21-Feb-2019 19:00"), Comment = "fix broken migrations", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 123667 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("21-Feb-2019"), StartTime = TimeSpan.Parse("17:59"), EndTime = TimeSpan.Parse("18:59"), Description = "Configure Infiniti : REBUS-188", TaskID = 123674 },
                new TaskLog { LogDate = DateTime.Parse("22-Feb-2019"), StartTime = TimeSpan.Parse("08:49"), EndTime = TimeSpan.Parse("10:49"), Description = "Configure Infiniti : REBUS-188", TaskID = 123674,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77109, CheckInTime = DateTime.Parse("24-Feb-2019 22:51"), Comment = "feat : add form mappings for UOM Idea and UOM IP Disclosure forms", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 123674 }, new TaskCheckIn { TaskID = 123675 }, new TaskCheckIn { TaskID = 122971 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("22-Feb-2019"), StartTime = TimeSpan.Parse("10:49"), EndTime = TimeSpan.Parse("13:49"), Description = "Configure Infiniti : REBUS-188", TaskID = 123675,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77110, CheckInTime = DateTime.Parse("24-Feb-2019 23:03"), Comment = "feat (forms): add blank UOM Idea and UOM IP Decision form", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 123674 }, new TaskCheckIn { TaskID = 123675 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("22-Feb-2019"), StartTime = TimeSpan.Parse("14:49"), EndTime = TimeSpan.Parse("16:45"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("25-Feb-2019"), StartTime = TimeSpan.Parse("09:00"), EndTime = TimeSpan.Parse("10:30"), Description = "Test Proposal Budget form : REBUS-445" },
                new TaskLog { LogDate = DateTime.Parse("25-Feb-2019"), StartTime = TimeSpan.Parse("10:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("25-Feb-2019"), StartTime = TimeSpan.Parse("11:30"), EndTime = TimeSpan.Parse("13:30"), Description = "Investigate Ethics page in Project View screen: REBUS-187", TaskID = 124006 },
                new TaskLog { LogDate = DateTime.Parse("25-Feb-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("14:40"), Description = "Backlog Refinement meeting" },
                new TaskLog { LogDate = DateTime.Parse("25-Feb-2019"), StartTime = TimeSpan.Parse("14:40"), EndTime = TimeSpan.Parse("17:30"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("26-Feb-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("11:00"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77173, CheckInTime = DateTime.Parse("26-Feb-2019 10:38"), Comment = "feat (forms): blank forms UoM Idea and UoM IP Disclosure, but with project selection drop-down added", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 122971 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("26-Feb-2019"), StartTime = TimeSpan.Parse("11:00"), EndTime = TimeSpan.Parse("12:30"), Description = "Temporary fix of Ethics page in Project View screen: REBUS-187", TaskID = 124006,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 77184, CheckInTime = DateTime.Parse("26-Feb-2019 13:18"), Comment = "fix (project view [external]): add upload button on ethic clearance page - just partial fix as label is incorrect", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124006 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("26-Feb-2019"), StartTime = TimeSpan.Parse("13:00"), EndTime = TimeSpan.Parse("14:05"), Description = "Sprint Review meeting" },
                new TaskLog { LogDate = DateTime.Parse("26-Feb-2019"), StartTime = TimeSpan.Parse("14:05"), EndTime = TimeSpan.Parse("15:05"), Description = "Fix position of label on check box on project details tab: REBUS-187", TaskID = 124059,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 77195, CheckInTime = DateTime.Parse("26-Feb-2019 15:33"), Comment = "fix (project viewer [external]): change position of label and checkbox on details tab ", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 124059 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("26-Feb-2019"), StartTime = TimeSpan.Parse("15:05"), EndTime = TimeSpan.Parse("16:50"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("27-Feb-2019"), StartTime = TimeSpan.Parse("09:30"), EndTime = TimeSpan.Parse("11:30"), Description = "Sprint planning meeting" },
                new TaskLog { LogDate = DateTime.Parse("27-Feb-2019"), StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Intelidox training" },
                new TaskLog { LogDate = DateTime.Parse("28-Feb-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("12:45"), Description = "Investigate Foundational Model #3 : REBUS-188", TaskID = 122971 },
                new TaskLog { LogDate = DateTime.Parse("28-Feb-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("17:15"), Description = "Connect a record to a container : REBUS-600", TaskID = 124390 }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write("#");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }

        private static void LoadTaskLogsJan(DbMigratorContext db)
        {
            Console.Write("Loading Task Logs for January ");

            List<TaskLog> taskLogs = new List<TaskLog>
            {
                                new TaskLog { LogDate = DateTime.Parse("02-Jan-2019"), StartTime = TimeSpan.Parse("08:05"), EndTime = TimeSpan.Parse("13:05"), Description = "Correct behaviour of currency text boxes", TaskID = 118102 },
                new TaskLog { LogDate = DateTime.Parse("02-Jan-2019"), StartTime = TimeSpan.Parse("13:35"), EndTime = TimeSpan.Parse("18:02"), Description = "Correct behaviour of currency text boxes", TaskID = 118102 },
                new TaskLog { LogDate = DateTime.Parse("03-Jan-2019"), StartTime = TimeSpan.Parse("08:05"), EndTime = TimeSpan.Parse("10:41"), Description = "Correct behaviour of currency text boxes", TaskID = 118102,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 75626, CheckInTime = DateTime.Parse("03-Jan-2019 10:39"), Comment = "fix (milestones): updated autoNumeric to latest version to get better handling of -ve numbers", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 118102 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("03-Jan-2019"), StartTime = TimeSpan.Parse("10:41"), EndTime = TimeSpan.Parse("12:48"), Description = "Changes to ensure edit button only needs to be clicked once", TaskID = 118993,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 75630, CheckInTime = DateTime.Parse("03-Jan-2019 12:48"), Comment = "fix (milestones): changes to ensure edit button only needs to be clicked once", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 118993 } } }
                     }},
                new TaskLog { LogDate = DateTime.Parse("03-Jan-2019"), StartTime = TimeSpan.Parse("13:19"), EndTime = TimeSpan.Parse("14:59"), Description = "Ensure 'Create Transaction' chevron is visible when milestone status is 'Not Achieved'", TaskID = 116503,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 75638, CheckInTime = DateTime.Parse("03-Jan-2019 14:56"), Comment = "fix (milestone wizard): create transaction step chevron should not be hidden when income milestone status set to 'Not Achieved'", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn>{ new TaskCheckIn { TaskID = 116503 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("03-Jan-2019"), StartTime = TimeSpan.Parse("14:59"), EndTime = TimeSpan.Parse("18:01"), Description = "Fix to enable process 2nd payment for Income milestone", TaskID = 117910 },
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("08:06"), EndTime = TimeSpan.Parse("09:10"), Description = "Fix to enable process 2nd payment for Income milestone; correct format of bar chart on financial summary page", TaskID = 117910,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 75648, CheckInTime = DateTime.Parse("04-Jan-2019 09:07"), Comment = "fix (milestones): - option to process 2nd income payment now available when income milestone has been partially paid", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn>{ new TaskCheckIn { TaskID = 117910 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("09:10"), EndTime = TimeSpan.Parse("11:38"), Description = "Fix to enable process 2nd payment for Income milestone; correct format of bar chart on financial summary page", TaskID = 117940,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 75670, CheckInTime = DateTime.Parse("04-Jan-2019 15:48"), Comment = "fix (Grants): Draining an income milestone does not clear the milestone amount to zero any longer", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 117940 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("11:38"), EndTime = TimeSpan.Parse("12:42"), Description = "Correct format of bar chart on financial summary page", TaskID = 113757,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn { ID = 75653, CheckInTime = DateTime.Parse("04-Jan-2019 10:38"), Comment = "fix (Grants - financial summary): financial summary graph now showing paid and unpaid amounts on the same row", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 113757 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("12:42"), EndTime = TimeSpan.Parse("13:47"), Description = "Fix to enable process 2nd payment for Income milestone; correct format of bar chart on financial summary page", TaskID = 108437 },
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("14:19"), EndTime = TimeSpan.Parse("15:33"), Description = "Investigate cause of advanced filter not able to be shown", TaskID = 118914 },
                new TaskLog { LogDate = DateTime.Parse("04-Jan-2019"), StartTime = TimeSpan.Parse("15:33"), EndTime = TimeSpan.Parse("16:25"), Description = "Correct behaviour of reporting database export", TaskID = 112925 },
                new TaskLog { LogDate = DateTime.Parse("07-Jan-2019"), StartTime = TimeSpan.Parse("08:10"), EndTime = TimeSpan.Parse("12:25"), Description = "Correct behaviour of reporting database export", TaskID = 112925 },
                new TaskLog { LogDate = DateTime.Parse("07-Jan-2019"), StartTime = TimeSpan.Parse("12:25"), EndTime = TimeSpan.Parse("12:46"), Description = "Investigate blank email sent on milestone revision", TaskID = 114032 },
                new TaskLog { LogDate = DateTime.Parse("07-Jan-2019"), StartTime = TimeSpan.Parse("13:16"), EndTime = TimeSpan.Parse("17:23"), Description = "Develop database migration for budget category data", TaskID = 116733 },
                new TaskLog { LogDate = DateTime.Parse("08-Jan-2019"), StartTime = TimeSpan.Parse("08:12"), EndTime = TimeSpan.Parse("13:48"), Description = "Develop database migration for budget category data", TaskID = 116733 },
                new TaskLog { LogDate = DateTime.Parse("08-Jan-2019"), StartTime = TimeSpan.Parse("14:18"), EndTime = TimeSpan.Parse("17:18"), Description = "Develop database migration for budget category data", TaskID = 116733 },
                new TaskLog { LogDate = DateTime.Parse("09-Jan-2019"), StartTime = TimeSpan.Parse("08:14"), EndTime = TimeSpan.Parse("12:14"), Description = "Develop database migration for budget category data", TaskID = 116733,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 75751, CheckInTime = DateTime.Parse("09-Jan-2019 09:21"), Comment = "feat (migrations): Add migration for project defalut milestones", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116733 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("09-Jan-2019"), StartTime = TimeSpan.Parse("12:46"), EndTime = TimeSpan.Parse("17:22"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114824 },
                new TaskLog { LogDate = DateTime.Parse("10-Jan-2019"), StartTime = TimeSpan.Parse("08:17"), EndTime = TimeSpan.Parse("09:41"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114824 },
                new TaskLog { LogDate = DateTime.Parse("10-Jan-2019"), StartTime = TimeSpan.Parse("09:41"), EndTime = TimeSpan.Parse("13:13"), Description = "Fix problem with save process milestone data not being reloaded", TaskID = 115935 },
                new TaskLog { LogDate = DateTime.Parse("10-Jan-2019"), StartTime = TimeSpan.Parse("13:43"), EndTime = TimeSpan.Parse("13:53"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("10-Jan-2019"), StartTime = TimeSpan.Parse("13:53"), EndTime = TimeSpan.Parse("16:48"), Description = "Fix problem with save process milestone data not being reloaded", TaskID = 115935 },
                new TaskLog { LogDate = DateTime.Parse("10-Jan-2019"), StartTime = TimeSpan.Parse("16:48"), EndTime = TimeSpan.Parse("16:51"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("11-Jan-2019"), StartTime = TimeSpan.Parse("08:14"), EndTime = TimeSpan.Parse("12:57"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("11-Jan-2019"), StartTime = TimeSpan.Parse("13:29"), EndTime = TimeSpan.Parse("16:29"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("14-Jan-2019"), StartTime = TimeSpan.Parse("07:55"), EndTime = TimeSpan.Parse("12:39"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("14-Jan-2019"), StartTime = TimeSpan.Parse("13:10"), EndTime = TimeSpan.Parse("16:10"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("15-Jan-2019"), StartTime = TimeSpan.Parse("08:35"), EndTime = TimeSpan.Parse("11:54"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("15-Jan-2019"), StartTime = TimeSpan.Parse("12:30"), EndTime = TimeSpan.Parse("16:30"), Description = "Develop migration for new Reporting DB table 'MilestoneBudgetCategory'", TaskID = 114824,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 75901, CheckInTime = DateTime.Parse("15-Jan-2019 15:06"), Comment = "feat (milestones): add ability to save budget category amounts when creating new financial milestone", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 114824 }, new TaskCheckIn { TaskID = 114827 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("16-Jan-2019"), StartTime = TimeSpan.Parse("08:46"), EndTime = TimeSpan.Parse("13:39"), Description = "Develop migration for new Reporting DB table 'MilestoneBudgetCategory'", TaskID = 114824 },
                new TaskLog { LogDate = DateTime.Parse("16-Jan-2019"), StartTime = TimeSpan.Parse("14:11"), EndTime = TimeSpan.Parse("18:11"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827 },
                new TaskLog { LogDate = DateTime.Parse("17-Jan-2019"), StartTime = TimeSpan.Parse("08:41"), EndTime = TimeSpan.Parse("10:51"), Description = "Develop processing of Milestones to cater for Budget Categories", TaskID = 114827,
                    CheckIns = new List<CheckIn>
                        {
                            new CheckIn {  ID = 75955, CheckInTime = DateTime.Parse("17-Jan-2019 10:44"), Comment = "feat (milestones): add financial milestone now saves budget category breakdown", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                                TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 114827 } } }
                         }
                    },
                new TaskLog { LogDate = DateTime.Parse("17-Jan-2019"), StartTime = TimeSpan.Parse("10:51"), EndTime = TimeSpan.Parse("11:56"), Description = "Add functionality to enable adding new financaial milestone with budget category breakdown", TaskID = 116747 },
                new TaskLog { LogDate = DateTime.Parse("17-Jan-2019"), StartTime = TimeSpan.Parse("12:27"), EndTime = TimeSpan.Parse("17:06"), Description = "Add functionality to enable adding new financaial milestone with budget category breakdown", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("18-Jan-2019"), StartTime = TimeSpan.Parse("08:42"), EndTime = TimeSpan.Parse("12:42"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("18-Jan-2019"), StartTime = TimeSpan.Parse("13:15"), EndTime = TimeSpan.Parse("16:10"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("21-Jan-2019"), StartTime = TimeSpan.Parse("08:41"), EndTime = TimeSpan.Parse("13:10"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76062, CheckInTime = DateTime.Parse("21-Jan-2019 11:33"), Comment = "feat (application transfer): on milestone summary page of the wizard, show - instead of $0.00 for non-financial milestones", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116716 } } }
                    }},
                new TaskLog { LogDate = DateTime.Parse("21-Jan-2019"), StartTime = TimeSpan.Parse("13:41"), EndTime = TimeSpan.Parse("16:41"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("22-Jan-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:05"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("22-Jan-2019"), StartTime = TimeSpan.Parse("13:45"), EndTime = TimeSpan.Parse("18:45"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76132, CheckInTime = DateTime.Parse("22-Jan-2019 19:09"), Comment = "feat (application transfer wizard): initial work to include budget categories on initial financial milestones", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116716 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("23-Jan-2019"), StartTime = TimeSpan.Parse("08:45"), EndTime = TimeSpan.Parse("13:15"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("23-Jan-2019"), StartTime = TimeSpan.Parse("13:45"), EndTime = TimeSpan.Parse("16:45"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("24-Jan-2019"), StartTime = TimeSpan.Parse("09:03"), EndTime = TimeSpan.Parse("13:26"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("24-Jan-2019"), StartTime = TimeSpan.Parse("13:56"), EndTime = TimeSpan.Parse("18:56"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76210, CheckInTime = DateTime.Parse("24-Jan-2019 18:52"), Comment = "feat (application transfer wizard): budget category details captured during Milestone Summary step (but still not saved at the end of the wizard)", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116716 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("25-Jan-2019"), StartTime = TimeSpan.Parse("08:39"), EndTime = TimeSpan.Parse("12:20"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("25-Jan-2019"), StartTime = TimeSpan.Parse("12:52"), EndTime = TimeSpan.Parse("16:52"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("29-Jan-2019"), StartTime = TimeSpan.Parse("08:40"), EndTime = TimeSpan.Parse("13:30"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("29-Jan-2019"), StartTime = TimeSpan.Parse("14:00"), EndTime = TimeSpan.Parse("18:00"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716 },
                new TaskLog { LogDate = DateTime.Parse("30-Jan-2019"), StartTime = TimeSpan.Parse("08:38"), EndTime = TimeSpan.Parse("11:49"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76288, CheckInTime = DateTime.Parse("30-Jan-2019 11:29"), Comment = "feat (application transfer wizard): budget category details captured during Milestone Summary step", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116716 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("30-Jan-2019"), StartTime = TimeSpan.Parse("11:49"), EndTime = TimeSpan.Parse("15:00"), Description = "Changes to application transfer wizard to include creating milestone budget category break-down", TaskID = 116716,
                    CheckIns = new List<CheckIn>
                    {
                        new CheckIn {  ID = 76295, CheckInTime = DateTime.Parse("30-Jan-2019 13:15"), Comment = "feat (application transfer wizard): fix so that budget category breakdown shown only as required", CodeBranchID = db.CodeBranches.Where(c => c.Name == "DevNextNext").First().ID,
                            TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 116716 } } }
                    } },
                new TaskLog { LogDate = DateTime.Parse("30-Jan-2019"), StartTime = TimeSpan.Parse("15:40"), EndTime = TimeSpan.Parse("17:08"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("31-Jan-2019"), StartTime = TimeSpan.Parse("08:43"), EndTime = TimeSpan.Parse("12:43"), Description = "Develop unit tests for create new milestone", TaskID = 119328 },
                new TaskLog { LogDate = DateTime.Parse("31-Jan-2019"), StartTime = TimeSpan.Parse("13:13"), EndTime = TimeSpan.Parse("16:44"), Description = "Develop unit tests for create new milestone", TaskID = 119328 }
            };

            TaskLogBF bf = new TaskLogBF(db);
            foreach (TaskLog tasklog in taskLogs)
            {
                Console.Write(".");
                bf.Create(tasklog);
            }

            Console.WriteLine(" Done");

            threads.Task.Delay(500).Wait();
        }
    }
}
