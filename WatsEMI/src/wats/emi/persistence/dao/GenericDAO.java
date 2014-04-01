package wats.emi.persistence.dao;

import org.hibernate.Query;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.Session;

import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.util.List;
import java.util.logging.Logger;

public abstract class GenericDAO<T> {
    private static final Logger logger = Logger.getLogger(GenericDAO.class.getName());

    protected SessionFactory sessionFactory = null;
    protected String entityName = null;
    protected Session session = null;
    protected Transaction tx = null;

    protected GenericDAO(SessionFactory sessionFactory) {
        this.sessionFactory = sessionFactory;
        this.entityName = entityType();
    }

    protected GenericDAO(GenericDAO dao) {
        this(dao.sessionFactory);
        this.session = dao.session;
    }

    /**
     * Find out the entity name from the sub class name
     */
    private String entityType(){
        Type t = getClass().getGenericSuperclass();
        ParameterizedType pt = (ParameterizedType)t;
        Class<T> type = (Class)pt.getActualTypeArguments()[0];
        return type.getName();
    }


    public void sessionStart(){
        if(session == null){
            session = sessionFactory.openSession();
            transactionBegin();
        }
    }

    public void sessionClose(){
        if (session != null){
            if (!tx.wasRolledBack()) {
                transactionCommit();
            }

            session.close();
            session = null;
        }
        tx = null;
    }

    private void transactionBegin(){
        tx = session.beginTransaction();
    }

    private void transactionCommit(){
        tx.commit();
    }

    protected void transactionRollback(){
        tx.rollback();
    }

    public void evict(T obj) {
        session.evict(obj);
    }

    public int count() {
        int count = -1;
        try {
            sessionStart();
            Query query = session.createQuery("select count(*) from " + entityName);
            count = ((Number)query.uniqueResult()).intValue();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

        }
        finally{
            sessionClose();
        }

        return count;
    }

    public int countEx() throws Exception {
        int count = -1;
        try {
            Query query = session.createQuery("select count(*) from " + entityName);
            count = ((Number)query.uniqueResult()).intValue();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;
        }

        return count;
    }

    public T find(int id) throws Exception {
        T obj = null;
        try {
            sessionStart();
            obj = (T)session.get(entityName, id);

        } catch (Exception e) {
            logger.severe(e.getMessage());
            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }
        return obj;
    }

    public T findEx(int id) throws Exception {
        T obj = null;
        try {
            obj = (T)session.get(entityName, id);

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        }
        return obj;
    }

    public List<T> findAll() throws Exception {
        List<T> objList = null;
        try {
            sessionStart();
            objList = (List<T>)session.createQuery("FROM " + entityName).list();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }
        return objList;
    }

    public List<T> findAllEx() throws Exception {
        List<T> objList = null;
        try {
            objList = (List<T>)session.createQuery("FROM " + entityName ).list();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;
        }
        return objList;
    }

    public List query(String hql) throws Exception {
        List objList = null;
        try {
            sessionStart();
            objList = session.createQuery(hql).list();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }
        return objList;
    }


    public List queryEx(String hql) throws Exception {
        List objList = null;
        try {
            objList = session.createQuery(hql).list();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        }
        return objList;
    }

    public int executeUpdate(String hql) throws Exception {
        int affects = -1;
        try {
            sessionStart();
            affects = session.createQuery(hql).executeUpdate();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }

        return affects;
    }

    public int executeUpdateEx(String hql) throws Exception {
        int affects = -1;
        try {
            affects = session.createQuery(hql).executeUpdate();

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;
        }

        return affects;
    }

    public boolean createOrUpdate(T object, boolean isNew) throws Exception {
        boolean succeeded = false;
        try {
            sessionStart();
            if (isNew)
                session.save(object);
            else
                session.update(object);
            succeeded = true;

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }
        return succeeded;
    }

    public boolean createOrUpdateEx(T object, boolean isNew) throws Exception {
        boolean succeeded = false;
        try {
            if (isNew)
                session.save(object);
            else
                session.update(object);
            session.flush();
            succeeded = true;

        } catch (Exception e) {
            logger.severe(e.getMessage());
            e.printStackTrace();
            transactionRollback();

            throw e;
        }
        return succeeded;
    }

    public boolean merge(T object) throws Exception {
        boolean succeeded = false;
        try {
            sessionStart();
            session.merge(object);
            succeeded = true;

        } catch (Exception e) {
            logger.severe(e.getMessage());
            e.printStackTrace();
            transactionRollback();

            throw e;

        } finally {
            sessionClose();
        }
        return succeeded;
    }

    public boolean mergeEx(T object) throws Exception {
        boolean succeeded = false;
        try {
            session.merge(object);
            succeeded = true;
            session.flush();

        } catch (Exception e) {
            logger.severe(e.getMessage());
            e.printStackTrace();
            transactionRollback();

            throw e;
        }
        return succeeded;
    }

    public boolean delete(T object) throws Exception {
        boolean succeeded = false;
        try {
            sessionStart();
            session.delete(object);
            succeeded = true;

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;

        } finally {
            sessionClose();
        }
        return succeeded;
    }

    public boolean deleteEx(T object) throws Exception {
        boolean succeeded = false;
        try {
            session.delete(object);
            session.flush();
            succeeded = true;

        } catch (Exception e) {
            logger.severe(e.getMessage());

            transactionRollback();
            e.printStackTrace();

            throw e;
        }

        return succeeded;
    }
}
