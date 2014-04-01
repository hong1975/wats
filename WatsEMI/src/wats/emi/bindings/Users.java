package wats.emi.bindings;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;

@XmlRootElement(name = "Users")
@XmlType(name="users")
public class Users implements Serializable {
	private static final long serialVersionUID = 7706082991787760188L;
	
	private List<User> users = new ArrayList<User>();
	
	@XmlElement(name = "User")
	public List<User> getUsers() {
		return users;
	}

	public void addUser(User user) {
		users.add(user);
	}
}
