package wats.emi.persistence;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-29
 * Time: 下午11:21
 * To change this template use File | Settings | File Templates.
 */
@Entity
@Table(name = "USERS")
@SequenceGenerator(name="USER_SEQ", sequenceName="USERS_SEQUENCE")
public class _User {
    private long id;
    private String userId;
    private String password;
    private String role;
    private boolean locked;

    private List<_Task> tasks = new ArrayList<_Task>();

    @Id
    @GeneratedValue(strategy= GenerationType.SEQUENCE, generator="USER_SEQ")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Column(name = "USERID")
    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    @Column(name = "PASSWORD")
    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @Column(name = "ROLE")
    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    @Column(name = "LOCKED")
    public boolean isLocked() {
        return locked;
    }

    public void setLocked(boolean locked) {
        this.locked = locked;
    }

    @ManyToMany(cascade = CascadeType.ALL)
    @JoinTable(
            name="USER_TASK",
            joinColumns = @JoinColumn(name="USER_ID"),
            inverseJoinColumns = @JoinColumn(name="TASK_ID")
    )
    public List<_Task> getTasks() {
        return tasks;
    }

    protected void setTasks(List<_Task> tasks) {
        this.tasks = tasks;
    }
}
