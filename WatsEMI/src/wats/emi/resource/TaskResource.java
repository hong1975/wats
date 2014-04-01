package wats.emi.resource;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.logging.Logger;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.Response;

import wats.emi.bindings.*;
import wats.emi.bindings.AddTaskResult;
import wats.emi.db.ConnectionManager;
import wats.emi.utility.IDGenerator;
import wats.emi.utility.Utility;

@Path("/tasks")
public class TaskResource extends Resource {
	private static final Logger logger = Logger.getLogger(TaskResource.class.getName());
	
	@Context HttpServletRequest request;
	@Context HttpServletResponse response;
	
	private static String list2String(List<String> listValue) {
		String wholeStr = "";
		for (String str : listValue) {
			if (wholeStr.length() == 0)
				wholeStr += "(" + str + ")";
			else
				wholeStr += "," + "(" + str + ")";
		}
		
		return wholeStr;
	}
	
	private static List<String> String2List(String strValue) {
		List<String> strList = new ArrayList<String>();
		if (strValue != null) {
			String[] strArray = strValue.split(",");
			String user;
			for (String str : strArray) {
				if (str.trim().length() == 0)
					continue;
				
				user = str.substring(1, str.length() - 1);
				strList.add(user);
			}
		}
			
		return strList;
	}
	
	public static void main(String[] args) {
		List<String> strs = String2List("");
		for (String str : strs) {
			System.out.println(str);
		}
	}
	
	@GET
	@Produces({"application/json"})
	public Response getTasks() throws Exception {
		validateClient(request);
		
		Tasks tasks = new Tasks();
		Task task;
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				String userTaskTableName = request.getUserPrincipal().getName() + "_TASK";
				stmt = conn.createStatement();
				String sql = "SELECT * FROM TASK LEFT JOIN \"" + userTaskTableName
					+ "\" ON TASK.ID=\"" + userTaskTableName + "\".TASKID "
					+ "WHERE TASK.ID IS NOT NULL";
                logger.warning(sql);
				
	            ResultSet rs = stmt.executeQuery(sql);
				while (rs.next()) {
					task = new Task();
					
					task.setId(rs.getLong("ID"));
					task.setName(rs.getString("TaskName"));
					task.setRegionID(rs.getString("RegionID"));
					task.setDescription(rs.getString("Description"));
					task.setCreator(rs.getString("Creator"));
					task.setCreateTime(rs.getString("CreateTime"));
					task.setChannelSettingID(rs.getLong("ChannelSettingID"));
					task.setEquipmentParameterID(rs.getLong("EquipmentParameterID"));
					task.setLinkConfigurationID(rs.getLong("LinkConfigurationID"));
					List<String> sites = String2List(rs.getString("Sites"));
					task.setSites(sites);
					List<String> unAssignedSites = String2List(rs.getString("UnassignedSites"));
					task.setUnAssignedSites(unAssignedSites);
					List<String> analyzers = String2List(rs.getString("Analyzers"));
					task.setAnalyzers(analyzers);
					//List<String> managers = String2List(rs.getString("Managers"));
					//task.setManagers(managers);
					
					tasks.getTasks().add(task);
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		
		if (tasks.getTasks().size() == 0)
			return Response.status(Response.Status.NO_CONTENT).build();
		else
			return Response.status(Response.Status.OK).entity(tasks).build();
		
	}
	
	@POST
	@Path("{version}")
	@Consumes({"application/json"})
	@Produces({"application/json"})
	public Response createTask(@PathParam("version") int version, Task task) throws Exception {
		validateClient(request);
		
		if (GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), task.getRegionID()) == null)
			return Response.status(Response.Status.NOT_FOUND).build();
		
		/*
		if (version != GlobalRegion.instance().getVersion()) {
			logger.finest("Request ver=" + version + ", latest ver=" + GlobalRegion.instance().getVersion());
			return Response.status(Response.Status.CONFLICT).build();
		}*/
		
		Connection conn = ConnectionManager.getInstance();
		String createTime = Utility.getCurDateTimeStr();
		ResultSet rs = null;
		PreparedStatement pstmt = null;
        long id = IDGenerator.getID();
		String sql = "INSERT INTO TASK (ID, TaskName,Description, RegionID, Creator,CreateTime,"
			+ "ChannelSettingID,EquipmentParameterID,LinkConfigurationID,Sites,Analyzers) VALUES("
            + id + ","
			+ "'" + task.getName() + "',"
			+ "'" + task.getDescription() + "',"
			+ "'" + task.getRegionID() + "',"
			+ "'" + task.getCreator() + "',"
			+ "'" + createTime + "',"
			+ task.getChannelSettingID() + ","
			+ task.getEquipmentParameterID() + ","
			+ task.getLinkConfigurationID() + ","
			+ "'" + list2String(task.getSites()) + "',"
			+ "'" + list2String(task.getAnalyzers()) + "')";
        logger.warning(sql);
		
		if (conn != null) {
			try {
				logger.finest(sql);
				
				conn.setAutoCommit(false);
				
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();

                HashSet<String> users = new HashSet<String>();
                for (String user: task.getAnalyzers()) {
                    users.add(user);
                }

                Region projetRegion = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), task.getRegionID());
                for (String user : projetRegion.getManagers()) {
                    users.add(user);
                }

                int role;
                long userTaskId;
                for (String user : users) {
                    if (task.getAnalyzers().indexOf(user) >= 0
                        && projetRegion.getManagers().indexOf(user) >= 0)
                        role = 2;
                    else if (task.getAnalyzers().indexOf(user) >= 0)
                        role = 0;
                    else
                        role = 1;

                    userTaskId = IDGenerator.getID();
                    sql = "INSERT INTO \"" + user + "_TASK\" (ID, TaskID, Role, Status) VALUES ("
                        + userTaskId + ","
                        + id + ","
                        + role + ","
                        + 0 + ")";

                    pstmt = conn.prepareStatement(sql);
                    pstmt.executeUpdate();
                }

                projetRegion.getTasks().add(id);
                GlobalRegion.instance().storeRegion();

                conn.commit();

                AddTaskResult result = new AddTaskResult();
                result.setRegionVersion(GlobalRegion.instance().getVersion());
                result.setTaskID(id);
                return Response.status(Response.Status.ACCEPTED).entity(result).build();

			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		
		return Response.status(Response.Status.PRECONDITION_FAILED).build();
	}
	
	@DELETE
	@Path("{id}")
	@Consumes({"application/json"})
	public Response removeTask(@PathParam("id") long id) throws Exception {
		validateClient(request);
		
		Connection conn = ConnectionManager.getInstance();
		PreparedStatement pstmt = null;
		String sql = "SELECT * FROM task WHERE ID=" + id;
		if (conn != null) {
			try {
				logger.finest(sql);
				
				conn.setAutoCommit(false);
				pstmt = conn.prepareStatement(sql);
				ResultSet rs = pstmt.executeQuery();
				if (rs.next()) {
					String regionID = rs.getString("RegionID");
					sql = "DELETE FROM task WHERE ID=" + id;
					pstmt = conn.prepareStatement(sql);
					pstmt.execute();
					
					Region region = GlobalRegion.findRegionByID(GlobalRegion.instance().getRoot(), regionID);
					if (region != null) {
						for (int i = 0; i < region.getTasks().size(); i++ ) {
							if (region.getTasks().get(i).intValue() == id) {
								region.getTasks().remove(i);
								break;
							}
						}
					}

					conn.commit();
					return Response.status(Response.Status.ACCEPTED).build();
					
				} else {
					conn.rollback();
				} 
				
			} catch (SQLException e) {
				conn.rollback();
                e.printStackTrace();
				logger.warning(e.getMessage());
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		
		return Response.status(Response.Status.PRECONDITION_FAILED).build();
	}
	
	@PUT
	@Path("{id}")
	@Consumes({"application/json"})
	public Response updateTask(@PathParam("id") long id, UpdateTaskRequest updateRequest) throws Exception {
		validateClient(request);
		
		Connection conn = ConnectionManager.getInstance();
		ResultSet rs = null;
		PreparedStatement pstmt = null;
		if (conn == null) {
			return Response.status(Response.Status.PRECONDITION_FAILED).build();
		}
		
		List<String> managers = GlobalRegion.instance().getTaskManagers(GlobalRegion.instance().getRoot(), id);
		if (UpdateTaskType.AddAnalyzer.toString().equals(updateRequest.getType())
			|| UpdateTaskType.RemoveAnalyzer.toString().equals(updateRequest.getType())) {
			String sql = "SELECT Analyzers FROM task WHERE ID=" + id;
			try {
				logger.finest(sql);
				conn.setAutoCommit(false);
				pstmt = conn.prepareStatement(sql);
				rs = pstmt.executeQuery(sql);
				if (!rs.next()) {
					conn.rollback();
					return Response.status(Response.Status.PRECONDITION_FAILED).build();
				} 
				
				String strAnalyzers = rs.getString("Analyzers");
				List<String> analyzers = String2List(strAnalyzers);
				if (UpdateTaskType.AddAnalyzer.toString().equals(updateRequest.getType())) { //add analyzers
					List<String> addedAnalyzers = new ArrayList<String>();
					for (String analyzer : updateRequest.getAnalyzers()) {
						if (analyzers.indexOf(analyzer) == -1) {
							addedAnalyzers.add(analyzer);
							analyzers.add(analyzer);
						}
					}
					
					if (addedAnalyzers.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
					
					int role;
					for (String addedAnalyzer : addedAnalyzers) {
						if (managers.indexOf(addedAnalyzer) != -1) {
							role = 2;
							sql = "UPDATE `" + addedAnalyzer + "_task` SET role=2 WHERE TaskID=" + id;

						} else {
							role = 0;
							sql = "INSERT INTO `" + addedAnalyzer + "_task` (TaskID, Role, Status) VALUES ("
	        					+ id + ","
	        					+ role + ","
	        					+ 0 + ")";
						}
						
						pstmt = conn.prepareStatement(sql);
						pstmt.executeUpdate();
						
					}
					
				} else { // remove analyzers
					List<String> removedAnalyzers = new ArrayList<String>();
					for (String analyzer : updateRequest.getAnalyzers()) {
						if (analyzers.indexOf(analyzer) >= 0) {
							removedAnalyzers.add(analyzer);
							analyzers.remove(analyzer);
						}
					}
					
					if (removedAnalyzers.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
					
					for (String removedAnalyzer : removedAnalyzers) {
						if (managers.indexOf(removedAnalyzer) != -1) {
							sql = "UPDATE `" + removedAnalyzer + "_task` SET role=1 WHERE TaskID=" + id;

						} else {
							sql = "DELETE FROM `" + removedAnalyzer + "_task` WHERE TaskID=" + id;
						}
						
						pstmt = conn.prepareStatement(sql);
						pstmt.executeUpdate();
						
					}
					
				}
				
				sql = "UPDATE `task` SET Analyzers='" + list2String(analyzers) + "' WHERE ID=" + id;
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();
				conn.commit();
				
				return Response.status(Response.Status.ACCEPTED).build();
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
			
			
		} else if (UpdateTaskType.AddSite.toString().equals(updateRequest.getType())
			|| UpdateTaskType.RemoveSite.toString().equals(updateRequest.getType())) {
			String sql = "SELECT * FROM task WHERE ID=" + id;
			try {
				logger.finest(sql);
				
				conn.setAutoCommit(false);
				
				pstmt = conn.prepareStatement(sql);
				rs = pstmt.executeQuery(sql);
				if (!rs.next()) {
					conn.rollback();
					return Response.status(Response.Status.PRECONDITION_FAILED).build();
				}
				String strSites = rs.getString("Sites");
				List<String> sites = String2List(strSites);
				if (UpdateTaskType.AddSite.toString().equals(updateRequest.getType())) { //add sites
					List<String> addedSites = new ArrayList<String>();
					for (String site : updateRequest.getSites()) {
						if (sites.indexOf(site) == -1) {
							addedSites.add(site);
							sites.add(site);
						}
					}
					
					if (addedSites.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
					
				} else { // remove sites
					List<String> removedSites = new ArrayList<String>();
					for (String site : updateRequest.getSites()) {
						if (sites.indexOf(site) >= 0) {
							removedSites.add(site);
							sites.remove(site);
						}
					}
					
					if (removedSites.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
				}
				
				sql = "UPDATE `Task` SET Sites='" + list2String(sites) + "' WHERE ID=" + id;
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();
				conn.commit();
				
				return Response.status(Response.Status.ACCEPTED).build();
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
			
		} else if (UpdateTaskType.AddUnassignedSite.toString().equals(updateRequest.getType())
			|| UpdateTaskType.RemoveUnassignedSite.toString().equals(updateRequest.getType())) {
			String sql = "SELECT * FROM task WHERE ID=" + id;
			try {
				logger.finest(sql);
				
				conn.setAutoCommit(false);
				
				pstmt = conn.prepareStatement(sql);
				rs = pstmt.executeQuery(sql);
				if (!rs.next()) {
					conn.rollback();
					return Response.status(Response.Status.PRECONDITION_FAILED).build();
				}
				String strSites = rs.getString("UnassignedSites");
				List<String> unAssignedSites = String2List(strSites);
				if (UpdateTaskType.AddUnassignedSite.toString().equals(updateRequest.getType())) { //add sites
					List<String> addedSites = new ArrayList<String>();
					for (String site : updateRequest.getSites()) {
						if (unAssignedSites.indexOf(site) == -1) {
							addedSites.add(site);
							unAssignedSites.add(site);
							
							sql = "INSERT INTO SITES (SiteID) VALUES('" + site +  "')";
							try {
                                pstmt = conn.prepareStatement(sql);
							    pstmt.executeUpdate();
                            } catch (Exception e) {
                                e.printStackTrace();
                            }
						}
					}
					
					if (addedSites.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
					
				} else { // remove sites
					List<String> removedSites = new ArrayList<String>();
					for (String site : updateRequest.getSites()) {
						if (unAssignedSites.indexOf(site) >= 0) {
							removedSites.add(site);
							unAssignedSites.remove(site);
						}
					}
					
					if (removedSites.size() == 0) {
						return Response.status(Response.Status.NOT_MODIFIED).build();
					}
				}
				
				sql = "UPDATE TASK SET UnassignedSites='" + list2String(unAssignedSites) + "' WHERE ID=" + id;
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();
				
				conn.commit();
				
				return Response.status(Response.Status.ACCEPTED).build();
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		} else if (UpdateTaskType.AcceptUnAssignedSite.toString().equals(updateRequest.getType())) {
			String sql = "SELECT Sites, UnassignedSites FROM task WHERE ID=" + id;
			try {
				logger.finest(sql);
				
				conn.setAutoCommit(false);
				
				pstmt = conn.prepareStatement(sql);
				rs = pstmt.executeQuery(sql);
				if (!rs.next()) {
					conn.rollback();
					return Response.status(Response.Status.PRECONDITION_FAILED).build();
				}
				String strUnAssignedSites = rs.getString("UnassignedSites");
				List<String> unAssignedSites = String2List(strUnAssignedSites);
				
				String strSites = rs.getString("Sites");
				List<String> sites = String2List(strSites);
				
				HashSet<String> acceptSites = new HashSet<String>();
				for(String acceptSite : updateRequest.getSites()) {
					if (unAssignedSites.contains(acceptSite)) {
						acceptSites.add(acceptSite);
						unAssignedSites.remove(acceptSite);
						
						if (!sites.contains(acceptSite)) {
							sites.add(acceptSite);
						}
					}
				}
				
				sql = "UPDATE `Task` SET UnassignedSites='" + list2String(unAssignedSites) + ", Sites='" + list2String(sites) + "' WHERE ID=" + id;
				logger.finest(sql);
				pstmt = conn.prepareStatement(sql);
				pstmt.executeUpdate();
				conn.commit();
				
				return Response.status(Response.Status.ACCEPTED).build();
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
			
		} else if (UpdateTaskType.RenameTask.toString().equals(updateRequest.getType())) {
			String sql = "UPDATE task SET TaskName='" + updateRequest.getName() + "' WHERE ID=" + id;
			Statement stmt = null;
			try {
				stmt = conn.createStatement();
				stmt.execute(sql);
				if (stmt.getUpdateCount() == 1) {
					return Response.status(Response.Status.ACCEPTED).build();
				} else {
					return Response.status(Response.Status.NOT_MODIFIED).build();					
				}
				
			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (pstmt != null) {
					try {
						pstmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
			
		}else {
			return Response.status(Response.Status.FORBIDDEN).build();
		}
	}
	
	@GET
	@Path("{id}/reports")
	@Produces({"application/json"})
	public Response getReports(@PathParam("id") long id) throws Exception {
		validateClient(request);
		
		Reports reports = new Reports();
		Report report;
		Connection conn = ConnectionManager.getInstance();
		Statement stmt = null;
		if (conn != null) {
			try {
				stmt = conn.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE, ResultSet.CONCUR_READ_ONLY);
				String sql = "SELECT * FROM TASKREPORT WHERE TaskID=" + id + " ORDER BY ReportTime ASC";
                logger.warning(sql);
				
				LimitSetting limitSetting;
	            ResultSet rs = stmt.executeQuery(sql);
				while (rs.next()) {
					report = new Report();
					
					report.setId(rs.getLong("ID"));
					report.setReportTime(rs.getString("ReportTime"));
					report.setAnalyzer(rs.getString("Analyzer"));
					report.setPairReport(rs.getInt("IsPairReport") == 1);
					report.setTaskID(rs.getLong("TaskID"));
					report.setEmiFileID(rs.getLong("EmiFileID"));
					report.setChannelSettingID(rs.getLong("ChannelSettingID"));

                    logger.warning("report: id=" + report.getId() + ", taskId=" + report.getTaskID());

					limitSetting = new LimitSetting();
					limitSetting.setChannelPowerLimit(rs.getInt("ChannelPowerLimit"));
					limitSetting.setDeltaPowerLimit(rs.getInt("DeltaPowerLimit"));
					limitSetting.setUseChannelPowerLimit(rs.getInt("UseChannelPowerLimit") == 1);
					limitSetting.setUseDeltaPowerLimit(rs.getInt("UseDeltaPowerLimit") == 1);
					report.setLimitSetting(limitSetting);
					
					report.setSpan(rs.getDouble("Span"));
					report.setStartFreq(rs.getDouble("StartFreq"));
					report.setEndFreq(rs.getDouble("EndFreq"));
					report.setChannelPreferred(rs.getInt("IsChannelPreferred") == 1);
					report.setDisplayChannel(rs.getInt("IsDisplayChannel") == 1);
					
					reports.getReports().add(report);
				}
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
                        e.printStackTrace();
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		
		return Response.status(Response.Status.OK).entity(reports).build();
	}
	
	@POST
	@Path("{id}/reports")
	@Consumes({"application/json"})
	@Produces({"application/json"})
	public Response uploadReport(@PathParam("id") long id, Report report) throws Exception {
		validateClient(request);
		
		Connection conn = ConnectionManager.getInstance();
		String createTime = Utility.getCurDateTimeStr();
		ResultSet rs = null;
		Statement stmt = null;
        long reportID = IDGenerator.getID();
		String sql = "INSERT INTO TASKREPORT (ID,TaskID,ReportTime, Analyzer, IsPairReport,EmiFileID,"
			+ "ChannelSettingID,UseChannelPowerLimit,UseDeltaPowerLimit,ChannelPowerLimit,DeltaPowerLimit,"
			+ "Span,StartFreq,EndFreq,IsChannelPreferred,IsDisplayChannel) VALUES("
            + reportID + ","
			+ report.getTaskID() + ","
			+ "'" + createTime + "',"
			+ "'" + report.getAnalyzer() + "',"
			+ (report.isPairReport() ? 1:0) + ","
			+ report.getEmiFileID() + ","
			+ report.getChannelSettingID() + ","
			+ (report.getLimitSetting().isUseChannelPowerLimit() ? 1:0) + ","
			+ (report.getLimitSetting().isUseDeltaPowerLimit() ? 1:0) + ","
			+ report.getLimitSetting().getChannelPowerLimit() + ","
			+ report.getLimitSetting().getDeltaPowerLimit() + ","
			+ report.getSpan() + ","
			+ report.getStartFreq() + ","
			+ report.getEndFreq() + ","
			+ (report.isChannelPreferred() ? 1:0) + ","
			+ (report.isDisplayChannel() ? 1:0) + ")";
        logger.warning(sql);
		
		if (conn != null) {
			try {
                stmt = conn.createStatement();
				stmt.executeUpdate(sql);

                report.setId(reportID);
				report.setReportTime(createTime);

                return Response.status(Response.Status.OK).entity(report).build();

			} catch (SQLException e) {
				conn.rollback();
				// TODO Auto-generated catch block
				logger.warning(e.getMessage());
                e.printStackTrace();
				throw e;
				
			} catch (Exception e) {
				logger.warning(e.getMessage());
				throw e;
				
			} finally {
				if (stmt != null) {
					try {
						stmt.close();
					} catch (Exception e) { /* ignore close errors */
	                }
				}
	            if (conn != null) {
	                try {
	                    conn.close();
	                } catch (Exception e) { /* ignore close errors */
	                }
	            }
	        }
		}
		
		return Response.status(Response.Status.PRECONDITION_FAILED).build();
	}

}
