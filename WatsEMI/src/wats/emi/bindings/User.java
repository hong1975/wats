package wats.emi.bindings;

import java.io.Serializable;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "User")
@XmlType(name="user")
public class User implements Serializable {
	private static final long serialVersionUID = 2616381683266679585L;
	
	private long id;
    private String userId;
    private String ha1;
    private String role;
    private boolean locked;
    
    public User() {
    	
    }
    
    public User(long id, String userId, String ha1, String role, boolean locked) {
    	this.id = id;
        this.userId = userId;
    	this.ha1 = ha1;
    	this.role = role;
    	this.locked = locked;
    	
    }

    @XmlElement(name = "id")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @XmlElement(name = "userId")
	public String getUserId() {
		return userId;
	}
	
    @XmlElement(name = "ha1")
	public String getHa1() {
		return ha1;
	}
	
    @XmlElement(name = "role")
	public String getRole() {
		return role;
	}
    
    @XmlElement(name = "locked")
	public boolean isLocked() {
		return locked;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public void setHa1(String ha1) {
		this.ha1 = ha1;
	}

	public void setRole(String role) {
		this.role = role;
	}

	public void setLocked(boolean locked) {
		this.locked = locked;
	}
}
