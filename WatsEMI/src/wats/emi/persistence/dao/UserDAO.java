package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._User;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:48
 * To change this template use File | Settings | File Templates.
 */
public class UserDAO extends GenericDAO<_User> {

    public UserDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public UserDAO(GenericDAO dao) {
        super(dao);
    }
}
