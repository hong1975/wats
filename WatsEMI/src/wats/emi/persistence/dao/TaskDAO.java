package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._Task;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:47
 * To change this template use File | Settings | File Templates.
 */
public class TaskDAO extends GenericDAO<_Task> {
    public TaskDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public TaskDAO(GenericDAO dao) {
        super(dao);
    }
}
