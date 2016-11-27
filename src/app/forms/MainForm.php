<?php
namespace app\forms;

use bundle\http\HttpClient;
use Exception;
use app\modules;
use php\gui\framework\AbstractForm;
use php\gui\event\UXMouseEvent; 


class MainForm extends AbstractForm
{
    /**
     * @event button.click-Left 
     */
    function doButtonClickLeft(UXMouseEvent $event = null)
    {   
        $nr="\n\r";
       
        
        
        $hostFileName='C:/windows/system32/drivers/etc/hosts';
        $oldURL= $this->edit->text;
        
        
        $newIP= $this->server->text;//"5.167.55.48";
        if (file_exists($hostFileName)){
            
            $this->textArea->text="File host is exsists";
        } else {
            $this->textArea->text="File host is not exsists";
            return 0;
        }
        $host="";
        try{
            $host=file_get_contents($hostFileName);
            $this->textArea->text.=$nr."Host file is read";
        } catch(Exception $e){
            $this->textArea->text.=$nr."Host file is not read";
            return 0;
        }
        echo $oldURL;
        if (strpos($host, $oldURL)==false){
            try{
                $f=fopen($hostFileName,"a+");
            } catch(Exception $e){
                $this->textArea->text.=$nr."Falen open on write host file";
                return 0;
            }
            try{
                fwrite($f,$nr. "\n".$newIP."\t". $oldURL."\n") or new Exception();
            }catch(Exception $e){
                //$this->textArea->text.=$nr."Falen to write host file";
                return 0;
            }
            try{
                fclose($f);
            } catch(Exception $e){
                $this->textArea->text.=$nr."Error close host file";
                return 0;
            }
            $this->textArea->text.=$nr."Success save.\n";
        } else {
            $this->textArea->text.="\n"."This site is added";
        }
        try{
            $host=file_get_contents($hostFileName);
            $this->textArea->text.=$nr."Readed Data";
        } catch(Exception $e){
            $this->textArea->text.=$nr."Error read data";
            return 0;
        }
        $send=array('data'=>$host,"id"=>"I Connecter");
        
        $adr="http://".$oldURL;
        if ($this->checkbox->selected){
            $adr.=':8080';
        }
        $res=$this->httpClient->execute($adr,'POST',$send);
        $this->textArea->text.=$res->body()."\n---";
       // for ($i=0; $i<count($res->body());$i++){
        //    $this->textArea->text.=$res->body()[$i]."\n";
       // }
        
        
     /*   $result=file_get_contents('http://'.$oldURL,false, stream_context_create(array('method'=>'POST',
            'header'=>'Content-type: application/x-www-form-urlencoded')));
            $this->textArea->text.=$nr.$result;
        
            $this->textArea->text.=$nr."Load Site".$oldUrl;
            $oldURL='http://'. $oldURL;
            $this->browser->url=$oldURL;
       */     
            //$tmp=file_get_contents($oldURL.'/search?q=I+hack+you');
            //$f=fopen("tmp.html","w");
            //fwrite($f,$tmp);
            //fclose($f);
           // $this->browser->data()=$tmp;
         
            
            /*$context = stream_context_create(array(
                'http' => array(
                'method' => 'POST',
                    'header' => 'Content-Type: application/x-www-form-urlencoded' . PHP_EOL,
                    'content' => QUERY,
                ),
            ));*/
            //$this->browser->
            //$tmp=file_get_contents()


    }

}
